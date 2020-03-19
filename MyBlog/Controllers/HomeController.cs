using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        //context for data base
        BlogContext db;
        public HomeController(BlogContext blogContext)
        {
            db = blogContext;
        }

        //index action that show list of posts
        public IActionResult Index()
        {
            return View(db.Posts);
        }

        //action return view with selected post
        public IActionResult Post(int? id)
        {
            var posts = db.Posts.Include(post => post.Comments);
            Post post = posts.FirstOrDefault(post => post.Id == id);
            if (post == null)
                return NotFound();
            ViewBag.Comments = db.Comments.Where(c => c.PostId == post.Id);
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(post);
        }

        //action for creating posts
        [HttpGet] 
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Post", new { post.Id });
        }

        //actions for deleting posts with confirming
        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
                return NotFound();
            return View(post);
        }
        //action binded by [Action] because cannot be same methods with save signature
        [HttpPost][ActionName("DeletePost")]
        public IActionResult ConfirmDeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
                return NotFound();
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //actions for edit selected post
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
                return NotFound();
            return View(post);
        }
        [HttpPost]
        public IActionResult EditPost(Post newPost)
        {
            db.Entry(newPost).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Post",new { newPost.Id });
        }

        //actions for add comment if reCaptcha is valid
        [HttpGet]
        public IActionResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            //chek comment for valid especially reCaptcha field
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            else
            {
                //if reCaptcha invalid send error message to post view to show message to user
                if(ModelState["g-recaptcha-response"].ValidationState != ModelValidationState.Valid)
                    TempData["ErrorMessage"]="Feild reCaptcha chalange";
            }
            
            //back to same page
            return RedirectToAction("Post",new { id = comment.PostId });
        }



    }
}