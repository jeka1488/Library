using System;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
       
        // GET
        [AllowAnonymous]
        public IActionResult Index()
        {
            return null;
        }

        public IActionResult Create()
        {
            return null;

        }
        
        [HttpPost]
        public IActionResult Create(CreateBookVM createBookVm)
        {
            return null;
        }
        
        public IActionResult Edit([FromRoute]int id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult Edit(EditBookVM editBookVm)
        {
            return null;
        }

        public IActionResult Delete([FromRoute]int id)
        {
            return null;
        }
    }
}