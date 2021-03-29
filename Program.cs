using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Program started");

            Console.WriteLine("*****************************************");
            Console.WriteLine("** Welcome to the Blog Database System **");
            Console.WriteLine("*****************************************");
            Console.WriteLine("Choose an Option");
            Console.WriteLine("1) Display all Blogs");
            Console.WriteLine("2) Add Blog");
            Console.WriteLine("3) Create Post");
            Console.WriteLine("4) Display Posts");

            var choice = Console.ReadLine();
            var db = new BloggingContext();

            if (choice == "1")
            {
                logger.Info("User choice: 1 - Display all Blogs");
                try
                {
                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

            }
            else if (choice == "2")
            {
                logger.Info("User choice: 2 - Add Blogs");
                try
                {
                    // Create and save a new Blog
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();

                    var blog = new Blog { Name = name };

                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            else if (choice == "3")
            {
                logger.Info("User choice: 3 - Create Post");
                //promt user to select the blog they are posting to
                try
                {
                    Console.WriteLine("Select the Blog for your Post");
                    
                    //display blogs for options
                    var query = db.Blogs.OrderBy(b => b.BlogId);
                    foreach (var item in query)
                    {
                        Console.WriteLine($@"{item.BlogId}) {item.Name}");
                    }

                    //create the post
                    Post post = new Post();

                    //link post to blog
                    var blogID = int.Parse(Console.ReadLine());
                    post.BlogId = blogID;
                    
                    //TODO: once the blog is selected, the post details can be entered (title/content)
                    Console.WriteLine("Enter Post title");
                    post.Title = Console.ReadLine();

                    Console.WriteLine("Enter Post content");
                    post.Content = Console.ReadLine();

                    //TODO: Posts should be saved to the Posts table - add post to posts list in blog
                    db.AddPost(post);
                    logger.Info($"Post added - {post.Title}");
                    //TODO: All user errors should be handled 
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            else if (choice == "4")
            {
                logger.Info("User choice: 4 - Display Posts");
                //TODO: Prompt the user to select the blog they want to view
                //TODO: Once the Blog is selected, all Posts related to the selected blog should be display as well as the number of Posts
                //TODO: For each Post, display the Blog name, Post title and Post content
            }

            logger.Info("Program ended");
        }
    }
}
