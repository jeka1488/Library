﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using Library.Data;
 using Library.Models;
 using Library.ViewModels;

 namespace Library.Controllers.Helpers
{
    public class AuthorHelper : IAuthorHelper
    {
        private ApplicationDbContext _context;
        public AuthorHelper(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<AuthorVMWithBookCount> GetRange()
        {
            var authors = _context.Authors.ToList();
            var authorsVM = new List<AuthorVMWithBookCount>();
            foreach (var a in authors)
            {
                authorsVM.Add(new AuthorVMWithBookCount
                {
                    Name = a.Name,
                    Surname = a.Surname,
                    Patronymic = a.Patronymic,
                    Id = a.Id,
                    BookCount = _context.Books.Count(b => b.AuthorId == a.Id)
                });
            }
            return authorsVM;
        }

        public AuthorVM GetAuthorVM()
        {
            return new AuthorVM();
        }

        public AuthorVM GetAuthorVM(int id)
        {
            var author = _context.Authors.Find(id);
            return new AuthorVM
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Patronymic = author.Patronymic
            };
        }

        public void Create(AuthorVM authorVm)
        {
            var author = new Author
            {
                Name = authorVm.Name,
                Surname = authorVm.Surname,
                Patronymic = authorVm.Patronymic
            };
            try
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Authors.Remove(author);
                Console.WriteLine("******* " + e.Message + " *******");
                throw;
            }
        }

        public void Edit(AuthorVM authorVm)
        {
            var author = _context.Authors.Find(authorVm.Id);
            author.Name = authorVm.Name;
            author.Surname = authorVm.Surname;
            author.Patronymic = authorVm.Patronymic;
            try
            {
                _context.Authors.Update(author);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("******* " + e.Message + " *******");
                throw;
            }
        }

        public void Delet(int id)
        {
            try
            {
                var author = _context.Authors.Find(id);
                _context.Authors.Remove(author);
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