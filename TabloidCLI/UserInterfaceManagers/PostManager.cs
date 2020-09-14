using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private Repositories.PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post ");
            Console.WriteLine(" 4)  Remove Post ");
            Console.WriteLine(" 5)  Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

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
                //case "5":
                    // NoteManagement();
                    //Console.WriteLine("Note Management to come");
                    //return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        public void List()
        {
            List<Post> posts = _postRepository.GetAll();    
            foreach (Post post in posts)
            {
                Console.WriteLine($"\n{post.Id} {post.Title}");
                Console.WriteLine($"{post.Author.FullName}");
                Console.WriteLine($"{post.Url}");
                Console.WriteLine($"{post.Author.FullName}");
                Console.WriteLine($"{post.Blog.Title}");
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public Post ChoosePost(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseAuthor(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> posts = _authorRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Author author = posts[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
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
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();

            // >>>>>>>>>>>>>>
            DateTime publishDate;
            while (true)
            {
                Console.WriteLine("Publication date (mm/dd/yyyy): ");
                bool allowed = DateTime.TryParse(Console.ReadLine(), out publishDate);
                if (allowed)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nPlease enter be valid publication date");
                }
            }

            post.PublishDateTime = publishDate;
            
            post.Author = ChooseAuthor("Select the post's author");

            post.Blog = ChooseBlog("Select the blog");

            _postRepository.Insert(post);
        }

        private static void stringOfTime(DateTime dt)
        {
            String.Format("{0:MM/dd/yyyy}", dt);  // "03/09/2008"
        }

        public void Edit()
        {
            Post postToEdit = ChoosePost("Which Post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged):\n> ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New Url (blank to leave unchanged):\n> ");
            string Url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Url))
            {
                postToEdit.Url = Url;
            }
            
            {
                postToEdit.PublishDateTime = DateTime.Now;
            }

            _postRepository.Update(postToEdit);
        }

        public void Remove()
        {
            Post postToDelete = ChoosePost("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}
