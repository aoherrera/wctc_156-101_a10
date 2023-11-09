using EFTutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Reflection.Metadata;

namespace EFTutorial.Services
{
    public class MainService : IMainService
    {

        public void Invoke()
        {
            string choice;
            do
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("\n1.) Display Blogs\n2.) Add Blog\n3.) Display Post\n" +
                    "4.) Add Post \n5.) Exit\n");
                choice = Console.ReadLine();

                //Display all blogs
                if (choice == "1")
                {
                    var db = new BlogContext();
                    db.DisplayBlogs();
                }

                else if (choice == "2")
                {
                    Console.WriteLine("Enter the blog name:");
                    var blogTitle = Console.ReadLine();

                    var db = new BlogContext();
                    db.AddBlog(blogTitle);
                }

                else if (choice == "3")
                {
                    var db = new BlogContext();
                    db.DisplayBlogs();

                    var minID = db.Blogs.Min(x => x.BlogId);
                    var maxID = db.Blogs.Max(x => x.BlogId);

                    var blogID = Helper.GetIntInRange("Enter the Blog ID to view post(s):", minID, maxID);
                    db.DisplayPosts(blogID);
                }

                else if (choice == "4")
                {
                    var db = new BlogContext();
                    db.DisplayBlogs();

                    var minID = db.Blogs.Min(x => x.BlogId);
                    var maxID = db.Blogs.Max(x => x.BlogId);

                    var blogID = Helper.GetIntInRange("Enter the Blog ID to add a post:", minID, maxID);
                    db.AddPost(blogID);
                }

                else if (choice == "5")
                    break;

                else
                    Console.WriteLine("Please enter a valid selection.\n");
            }
            while (choice != "5");
        }
    }
}