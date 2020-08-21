﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using Library.Data;
 using Library.Models;
 using Library.ViewModels;

 namespace Library.Controllers.Helpers
{
    public class BookHelper : IBookHelper
    {
        private readonly ApplicationDbContext _context;
        public BookHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<BookVM> GetRange()
        {
            var books = _context.Books.ToList();
            var booksVM = new List<BookVM>();
            foreach (var b in books)
            {
                var author = _context.Authors.FirstOrDefault(a => a.Id == b.AuthorId);
                if (author != null)
                {
                    booksVM.Add(new BookVM
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = author.Surname + " " + author.Name + " " + author.Patronymic
                    });
                }
                else
                {
                    booksVM.Add(new BookVM
                    {
                        Title = b.Title,
                    });
                }
            }
            return booksVM;
        }

        public CreateBookVM GetCreateBookVM()
        {
            var authors = _context.Authors.ToList();
            var authorsVM = new List<AuthorVM>();
            foreach (var a in authors)
            {
                authorsVM.Add(new AuthorVM
                {
                    Name = a.Name,
                    Surname = a.Surname,
                    Patronymic = a.Patronymic,
                    Id = a.Id
                });
            }
            return new CreateBookVM
            {
                AuthorId = 0,
                Title = "",
                Authors = authorsVM
            };
        }
        
        public EditBookVM GetEditBookVM(int id)
        {
            var authors = _context.Authors.ToList();
            var authorsVM = new List<AuthorVM>();
            foreach (var a in authors)
            {
                authorsVM.Add(new AuthorVM
                {
                    Name = a.Name,
                    Surname = a.Surname,
                    Patronymic = a.Patronymic,
                    Id = a.Id
                });
            }

            var book = _context.Books.Find(id);
            return new EditBookVM()
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Title = book.Title,
                Authors = authorsVM
            };
        }

        public void Create(CreateBookVM bookVM)
        {
            var book = new Book
            {
                Title = bookVM.Title,
                AuthorId = bookVM.AuthorId
            };
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("******* " + e.Message + " *******");
                _context.Books.Remove(book);
                throw;
            }
        }

       

        public void Edit(EditBookVM editBookVm)
        {
            var book = _context.Books.Find(editBookVm.Id);
            book.Title = editBookVm.Title;
            book.AuthorId = editBookVm.AuthorId;
            try
            {
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("******* " + e.Message + " *******");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("******* " + e.Message + " *******");
                throw;
            }
        }
    }
}