using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;

        }
        // The menu for the Blog portion of the the project
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Title and URL");
            Console.WriteLine(" 2) Add New Title and URL");
            Console.WriteLine(" 3) Edit Title and Url");
            Console.WriteLine(" 4) Remove Title and URL");
            Console.WriteLine(" 0) To Exit Blog");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        
        // Grabbing a list of blog entries from the database, and then iterating over them.
        // When this is called by hitting 1 in the options it will list the title and url associated with it.
        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }

        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        // Adding blog Titles and Url Posts
        private void Add()
        {
            Console.WriteLine("New Blog Post");
            Blog blog = new Blog();

            Console.Write("Title: ");
            blog.Title = Console.ReadLine();

            Console.Write("URL: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);
        }

        // Editing Blog Titles and URL. You will be given two prompts and the choice to leave them blank so you don't have to 
        //write anything
        private void Edit()
        {
            Blog blogToEdit = Choose("Choose a blog to edit.");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title: (blank to leave unchanged) ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.Write("New URL: (blank to leave unchanged) ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
           
               blogToEdit.Url = url;
           }

            _blogRepository.Update(blogToEdit);
        }
        // Removes the given typed in blog choice to be deleted.
        private void Remove()
        {
            Blog blogToDelete = Choose("Which author would you like to remove?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }
    }
}
