using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class SampleData
    {
        private static Random gen = new Random();
        static DateTime RandomDay()
        {
            DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public static void Initialize(BlogContext context)
        {
            if (!context.Posts.Any())
            {
                Comment c1 = new Comment { User = "Dmitry", Content="Comment 1 to Post 1",Date = RandomDay()};
                Comment c2 = new Comment { User = "Dmitr", Content="Comment 2 to Post 1", Date = RandomDay()};
                Comment c3 = new Comment { User = "Dmitry", Content="Comment 1 to Post 2", Date = RandomDay()};
                Comment c4 = new Comment { User = "Dmitry", Content="Comment 1 to Post 3", Date = RandomDay()};
                Comment c5 = new Comment { User = "Dmitry", Content = "Comment 1 to Post 4", Date = RandomDay()};

                context.Comments.Add(c1);
                context.Comments.Add(c2);
                context.Comments.Add(c3);
                context.Comments.Add(c4);
                context.Comments.Add(c5);

                string examplePost = @"<h1>Some Header</h1>

<h2>some subheader</h2>

<p>some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph&nbsp;some text with new paragraph</p>

<ul>
	<li>some list item 1</li>
	<li>some list item 2</li>
	<li>some list item 3</li>
	<li>some list item 4</li>
	<li>some list item 5</li>
</ul>

<p>some table&nbsp;</p>

<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 500px"">
     < tbody >
 
         < tr >
 
             < td > Header 1 </ td >
    
                < td > Header2 </ td >
    
            </ tr >
    
            < tr >
    
                < td > value </ td >
    
                < td > value </ td >
    
            </ tr >
    
            < tr >
    
                < td > value </ td >
    
                < td > value </ td >
    
            </ tr >
    
        </ tbody >
    </ table >
    

    < p > Some image </ p >
       

       < p >< img alt = """" src = ""https://media.sproutsocial.com/uploads/2017/02/10x-featured-social-media-image-size.png"" style = ""height:460px; width:780px"" /></ p >
              

              < p > &nbsp;</ p >
                 ";


                context.Posts.AddRange(
                    new Post 
                    {
                        Name = "My First Post",
                        Description = "My first post description",
                        Content = examplePost,
                        User = "Dmitry",
                        Comments = new List<Comment>{ c1, c2 },
                        Date = c1.Date.AddDays(-1 * gen.Next(1, 30))
                    },new Post
                    {
                        Name = "My Second Post",
                        Description = "My secont post description",
                        Content = examplePost,
                        User = "Dmitry",
                        Comments = new List<Comment> { c3 },
                        Date = c3.Date.AddDays(-1 * gen.Next(1,30)) 
                    },new Post 
                    {
                        Name = "My Third Post",
                        Description = "My third post description",
                        Content = examplePost,
                        User = "Dmitry",
                        Comments = new List<Comment>{ c4 },
                        Date = c4.Date.AddDays(-1 * gen.Next(1, 360))
                    },new Post 
                    {
                        Name = "My Fourth Post",
                        Description = "My fourth post description",
                        Content = examplePost,
                        User = "Dmitry",
                        Comments = new List<Comment>{ c5 },
                        Date = c5.Date.AddDays(-1 * gen.Next(1, 30))
                    });
                context.SaveChanges();


                


            }
        }
    }
}
