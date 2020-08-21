using System;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        
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
        public IActionResult Create(AuthorVM authorVm)
        {
            return null;
        }
        
        public IActionResult Edit([FromRoute]int id)
        {
            return null;
        }
       
        [HttpPost]
        public IActionResult Edit(AuthorVM authorVm)
        {
            return null;
        }
        
        public IActionResult Delete([FromRoute]int id)
        {
            return null;
        }
    }
}