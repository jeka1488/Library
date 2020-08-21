using System.Collections.Generic;

namespace Library.ViewModels
{
    public class EditBookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<AuthorVM> Authors { get; set; }
    }
}