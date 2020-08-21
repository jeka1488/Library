namespace Library.Models
    {
        /// <summary>
        /// Book model
        /// </summary>
        public class Book
        {
            //Primary key 
            public int Id { get; set; }
        
            public string Title { get; set; }
        
        
            //FK to table author
            public int AuthorId { get; set; }
        
            //Author many to one
            public Author Author { get; set; }
        }
    }