using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASpecInvestigation.Models;
using Microsoft.EntityFrameworkCore;

namespace ASpecInvestigation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new BloggingContext())
            {
                // this query works and filter for the first blog only.
                //var blogs = await db.Blogs
                //    .Include(s => s.Posts)
                //    .Where(s => s.Posts.Any(d => d.Title.Contains("First")))
                //    .ToListAsync();

                // this query with the equivalent filter logic, but it runs into exception.
                var blogs = await db.Blogs
                    .Include(s => s.Posts)
                    .Where(s => s.Posts.Any(Post.ContainsFirst))
                    .ToListAsync();

                // query must be changed to the following for it to work
                //var blogs = await db.Posts
                //    .Where(Post.ContainsFirst)
                //    .Select(d => d.Blog)
                //        .Include(s => s.Posts)
                //    .ToListAsync();

                foreach (var blog in blogs)
                {
                    Console.WriteLine($"Postings in {blog.Url}");

                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($"Post {post.Title} content {post.Content}");
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
