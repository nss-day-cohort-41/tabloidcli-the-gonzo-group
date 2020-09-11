﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;

        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1) List Journals");
            Console.WriteLine("2) Add Journal");
            Console.WriteLine("3) Edit Journal");
            Console.WriteLine("4) Remove Journal");
            Console.WriteLine("0) Go Back");

            Console.Write(">");
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
        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine(journal);
            }
        }
       private void Add()
        {
            Console.WriteLine("New Journal");
            Journal journal = new Journal();

            Console.Write("Title:");
            journal.Title = Console.ReadLine();

            Console.Write("Content:");
            journal.Content = Console.ReadLine();

            Console.Write("CreateDateTime");
            journal.CreateDateTime = Console.ReadLine();

            _journalRepository.Insert(journal);
        }
        private void Edit()
        {
            Journal journalToEdit = Choose("Which journal would you like to edit?");
            if(journalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged:");
            string Title = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(Title))
            {
                journalToEdit.Title = Title;
            }
            Console.Write("New Content (blank to leave unchanged:");
            string Content = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(Content))
            {
                journalToEdit.Content = Content;
            }
            Console.Write("New CreateDateTime (blank to leave unchanged:");
            DateTime CreateDateTime = Console.ReadLine();
            if(!DateTime.IsNullOrWhiteSpace(CreateDateTime))
            {
                journalToEdit.CreateDateTime = CreateDateTime;
            }
            _journalRepository.Update(journalToEdit);
        }
        private void Remove()
        {
            Journal journalToDelete = Choose("Which journal would you like to remove?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
        }
    }

