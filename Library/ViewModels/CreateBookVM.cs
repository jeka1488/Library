using System.Collections.Generic;

namespace Library.ViewModels
{
    public class CreateBookVM
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public List<AuthorVM> Authors { get; set; }
    }
}