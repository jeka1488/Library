using System.Collections.Generic;
using Library.Models.Library.Models;

namespace Library.Models
{
    /// <summary>
    /// Author model
    /// </summary>
    public class Author
    {
        
        //primary key  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        
        //Books one to many
        public ICollection<Book> Books { get; set; }
    }
}