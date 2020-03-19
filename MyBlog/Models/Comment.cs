using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class Comment : GoogleReCaptchaModelBase
    {
        // comment model
        public int Id { get; set; }
        public string User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        //use for foreign key
        public int? PostId { get; set; }
        public Post Post { get; set; }

        public Comment() 
        {
            //set current time on create
            Date = DateTime.Now;
        }
    }
}
