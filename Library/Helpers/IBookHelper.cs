using System.Collections.Generic;
using Library.Models;
using Library.ViewModels;

namespace Library.Controllers.Helpers
{
    public interface IBookHelper
    {
        public List<BookVM> GetRange();
        
        public CreateBookVM GetCreateBookVM();
        public EditBookVM GetEditBookVM(int id);
        public void Create(CreateBookVM bookVM);
        public void Edit(EditBookVM editBookVm);
        public void Delete(int id);
    }
}