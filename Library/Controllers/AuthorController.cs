using System;
using Library.Controllers.Helpers;
using Library.Data;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorHelper _authorHelper;
        public AuthorController(ApplicationDbContext context)
        {
            _authorHelper = new AuthorHelper(context);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_authorHelper.GetRange());
        }
        public IActionResult Create()
        {
            return View(_authorHelper.GetAuthorVM());
        }
        [HttpPost]
        public IActionResult Create(AuthorVM authorVm)
        {
            try
            {
                _authorHelper.Create(authorVm);
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit([FromRoute]int id)
        {
            return View(_authorHelper.GetAuthorVM(id));
        }
       
        [HttpPost]
        public IActionResult Edit(AuthorVM authorVm)
        {
            try
            {
                _authorHelper.Edit(authorVm);
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
                _authorHelper.Delet(id);
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Index");
        }
    }
}