using System;
using System.Collections.Generic;
using EFTutorial.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFTutorial.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs {get;set;}
        public DbSet<Post> Posts {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("BloggingContext"));
        }

        public void DisplayBlogs()
        {
            using (var db = new BlogContext())
            {
                var blogsList = db.Blogs.ToList(); // convert to List from IQueryable

                if (!blogsList.Any())
                {
                    Console.WriteLine("There are no blogs in the database.");
                    
                }

                else
                {
                    foreach (var b in blogsList)
                    {
                        Console.WriteLine($"Blog ID {b.BlogId}: {b.Name}");
                    }
                }

            }
        }

        public void AddBlog(string blogTitle)
        {
            var blog = new Blog()
            {
                Name = Helper.ConvertTitle(blogTitle)
            };

            using (var db = new BlogContext())
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
            }

            Console.WriteLine($"Blog \"{blog.Name}\" added.");
        }

        public void DisplayPosts(int blogID)
        {
            using (var db = new BlogContext())
            {
                var postsList = db.Posts.Where(x => x.BlogId == blogID).ToList();

                if (postsList.Count() != 0)
                {
                    Console.WriteLine($"Total Posts: {postsList.Count()}\n");
                    foreach (var p in postsList)
                    {
                        Console.WriteLine($"\t{p.Blog.Name} | {p.Title}");
                        Console.WriteLine($"\t{p.Content}\n");
                    }
                }
                else
                    Console.WriteLine($"The blog does not contain any posts.");
            }
        }

        public void AddPost(int blogID)
        {
            var postTitle = "";
            do
            {
                Console.WriteLine("Please enter a title for your post:");
                postTitle = Console.ReadLine();
                if (String.IsNullOrEmpty(postTitle.Trim()))
                {
                    Console.WriteLine("Post title cannot be empty.");
                    continue;
                }
            } while (String.IsNullOrEmpty(postTitle.Trim()));


            Console.WriteLine("Please enter the post content:");
            var postContent = Console.ReadLine();

            using (var db = new BlogContext())
            {
                var post = new Post()
                {
                    Title = postTitle,
                    Content = postContent,
                    BlogId = blogID
                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
    }

}