using System;
using Library.Controllers.Helpers;
using Library.Data;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly IBookHelper _bookHelper;
        public BookController(ApplicationDbContext context)
        {
            _bookHelper = new BookHelper(context);
        }
        
        // GET
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_bookHelper.GetRange());
        }

        public IActionResult Create()
        {
            return View(_bookHelper.GetCreateBookVM());

        }
        
        [HttpPost]
        public IActionResult Create(CreateBookVM createBookVm)
        {
            //TODO:Toast
            try
            {
                _bookHelper.Create(createBookVm);
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit([FromRoute]int id)
        {
            return View(_bookHelper.GetEditBookVM(id));
        }

        [HttpPost]
        public IActionResult Edit(EditBookVM editBookVm)
        {
            //TODO:Toast
            try
            {
                _bookHelper.Edit(editBookVm);
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
                _bookHelper.Delete(id);
            }
            catch (Exception e)
            {
            }

            return RedirectToAction("Index");
        }
    }
}