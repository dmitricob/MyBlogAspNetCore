using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ON DELETE SET NULL
            modelBuilder.Entity<Comment>()
                .HasOne<Post>(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
