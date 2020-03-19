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
        BlogContext db;

        public HomeController( BlogContext blogContext)
        {
            db = blogContext;
        }

        public IActionResult Index()
        {
            return View(db.Posts);
        }

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

        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
                return NotFound();
            return View(post);
        }

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


        [HttpGet]
        public IActionResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            else
            {
                if(ModelState["g-recaptcha-response"].ValidationState != ModelValidationState.Valid)
                    TempData["ErrorMessage"]="Feild reCaptcha chalange";
            }
            
            return RedirectToAction("Post",new { id = comment.PostId });
        }



    }
}