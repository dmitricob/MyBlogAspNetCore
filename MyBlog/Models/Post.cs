using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Post
    {
        // post model
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        // need to fk in Comment table
        public DateTime Date { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            //set current time on create
            Date = DateTime.Now;
            Comments = new List<Comment>();
        }
    }
}
