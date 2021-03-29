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
            Console.WriteLine("3) Display Posts");

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

            logger.Info("Program ended");
        }
    }
}
