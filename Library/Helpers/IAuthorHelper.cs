using System.Collections.Generic;
using Library.ViewModels;

namespace Library.Controllers.Helpers
{
    public interface IAuthorHelper
    {
        public List<AuthorVMWithBookCount> GetRange();
        public AuthorVM GetAuthorVM();
        public AuthorVM GetAuthorVM(int id);
        public void Create(AuthorVM authorVm);
        public void Edit(AuthorVM authorVm);
        
        public void Delet(int id);
    }
}