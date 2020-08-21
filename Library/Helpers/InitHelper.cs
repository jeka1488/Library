using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers.Helpers
{
    public class InitHelper
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole<int>> _roleManager;
        private UserManager<User> _userManager;

        public InitHelper(ApplicationDbContext applicationDbContext,
        RoleManager<IdentityRole<int>> roleManager,
            UserManager<User> userManager
        )
        {
            _context = applicationDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Init()
        {
            _context.Database.Migrate();

            if (!_roleManager.Roles.Any())
            {
                await CreateDefaultRoles();
            }

            if (!_userManager.Users.Any())
            {
                await CreateDefaultUsers();
            } 
            if (!_context.Authors.Any())
            {
                CreateDefaultAuthors();
            }
            if (!_context.Books.Any())
            {
                CreateDefaultBooks();
            }
        }

        private async Task CreateDefaultRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole<int>("admin"));
            await _roleManager.CreateAsync(new IdentityRole<int>("client"));
        }      
        
        private async Task CreateDefaultUsers()
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@mail.ru",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(admin, "123!Aa");
            await _userManager.AddToRoleAsync(admin, "admin");
                
            var client = new User
            {
                UserName = "client",
                Email = "client@mail.ru",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(client, "123!Aa");
            await _userManager.AddToRoleAsync(client, "client");
        }

        private void CreateDefaultAuthors()
        {
            _context.Authors.AddRange(new List<Author>
            {
                new Author
                {
                    Name = "A",
                    Patronymic = "S",
                    Surname = "Pushkin"
                },
                new Author
                {
                    Name = "F",
                    Patronymic = "M",
                    Surname = "Dostaevsky"
                }
            });
            _context.SaveChanges();
        } 
        
        private void CreateDefaultBooks()
        {
            var pushkin = _context.Authors.First(a => a.Surname == "Pushkin");
            if (pushkin != null)
            {
                _context.Books.AddRange(new List<Book>
                {
                    new Book()
                    {
                        Title = "Ruslan i Ludmila",
                        AuthorId = pushkin.Id
                    },
                    new Book
                    {
                        Title = "Zolotaya ribka",
                        AuthorId = pushkin.Id
                    }
                });
            }

            var dostaevsky = _context.Authors.First(a => a.Surname == "Dostaevsky");
            if (dostaevsky != null)
            {
                _context.Books.AddRange(new List<Book>
                {
                    new Book()
                    {
                        Title = "Prestuplenie i nakozanie",
                        AuthorId = dostaevsky.Id
                    },
                    new Book
                    {
                        Title = "Besi",
                        AuthorId = dostaevsky.Id
                    }
                });
            }
            _context.SaveChanges();
        }
    }
}