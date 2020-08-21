using System.Collections.Generic;
using System.Linq;
using Library.Data;
using Library.DTO;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers.Rest
{
    [ApiController]
    [Route("/api/v1")]
    public class BookRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BookRestController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// получение списка книг с именем автора
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
        public IActionResult Get()
        {
            var books = _context.Books.ToList();
            var booksDTO = new List<BookDTO>();
            foreach (var b in books)
            {
                var author = _context.Authors.FirstOrDefault(a => a.Id == b.AuthorId);
                if (author == null)
                {
                    continue;
                }
                booksDTO.Add(new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    Author = author.Surname
                });
            }
            return Ok(booksDTO);
        }
        
        /// <summary>
        /// создание книги
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]BookDTO bookDto)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == bookDto.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }
            var books = new Book
            {
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId
            };
            _context.Books.Add(books);
            return Ok();
        }
        
        /// <summary>
        /// получение данных книги по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromRoute]int id)
        { 
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            var author = _context.Authors.First(a => a.Id == book.Id);
            var bookDTO = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Author = author.Surname    
            };
            return Ok(bookDTO);
        }
        
        
        /// <summary>
        /// обновление данных книги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromRoute]int id, [FromBody]BookDTO bookDto)
        { 
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            var author = _context.Authors.First(a => a.Id == bookDto.AuthorId);
            if (author == null)
            {
                return NotFound();
            }

            book.Title = bookDto.Title;
            book.AuthorId = bookDto.AuthorId;
            _context.Books.Update(book);
            _context.SaveChanges();
            return Ok();
        } 
        
        
        /// <summary>
        /// удаление записи книги из БД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("book/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute]int id)
        { 
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
      
    }
}