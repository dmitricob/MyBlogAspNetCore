using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Date = DateTime.Now;
        }
    }
}
