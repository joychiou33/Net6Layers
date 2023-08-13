using ExamLayer.Data;
using ExamLayer.Models;
using ExamLayer.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            //Get Data from DB - Domain models
            var booksDomain = await _dbContext.Books.ToListAsync();
            //Map Domain models to DTOs
            var booksDto = new List<BookVM>();
            foreach (var bookDomain in booksDomain)
            {
                booksDto.Add(new BookVM()
                {
                    Name = bookDomain.Name,
                    Type = bookDomain.Type,
                    PublishDate = bookDomain.PublishDate,
                    Price = bookDomain.Price
                });
            }
            //Return DTOs
            
            return Ok(booksDto);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                return Ok(book);
            }          
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookVM item)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Type = item.Type,
                PublishDate = item.PublishDate,
                Price = item.Price
            };
            //await _dbContext.Books.AddAsync(book);
            //await _dbContext.SaveChangesAsync();
            await _bookRepository.CreateAsync(book);

            var bookDto = new BookVM
            {
                Name = book.Name,
                Type = book.Type,
                PublishDate = book.PublishDate,
                Price = book.Price
            };
            // 201
            //return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
            // 200
            return Ok(bookDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, BookVM item)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if(book != null)
            {
                book.Name = item.Name;
                book.Type = item.Type;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;

                await _dbContext.SaveChangesAsync();
                return Ok(book);
            }
            return NotFound(); 
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                _dbContext.Remove(book);
                await _dbContext.SaveChangesAsync();
                return Ok(book);
            }
            return NotFound();
        }
    }
}
