using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ASpecInvestigation.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=blogging.db;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey("BlogId");
            });


            SeedEntities(modelBuilder);
        }

        private void SeedEntities(ModelBuilder modelBuilder)
        {
            SeedBlogEntities(modelBuilder);
        }

        private void SeedBlogEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasData(new Blog { BlogId = 1, Url = "http://sample.com" });

            modelBuilder.Entity<Post>().HasData(
                new Post() { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });

            modelBuilder.Entity<Post>().HasData(
                new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });

            modelBuilder.Entity<Blog>().HasData(new Blog { BlogId = 2, Url = "http://www.custom-blog.com" });

            modelBuilder.Entity<Post>().HasData(
                new Post { BlogId = 2, PostId = 3, Title = "Custom blog post", Content = "Custom blog post content" });
        }
    }
}
