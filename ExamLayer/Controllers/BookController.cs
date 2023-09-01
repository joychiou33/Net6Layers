using ExamLayer.Data;
using ExamLayer.Filter;
using ExamLayer.Models;
using ExamLayer.Models.DTO;
using ExamLayer.Models.ViewModel;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        //private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            //var result = new PagingSearchOutput<List<BookDto>>();
            var books = await _bookService.GetAllAsync(filter);
            
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var book = await _bookService.GetAsync(id);
            if (book != null)
            {
                return Ok(book);
            }          
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QueryBookDto item)
        {
            var iResult = await _bookService.CreateAsync(item);
            if(iResult > 0) 
            {
                return Ok();
            }
            return BadRequest("Invalid model object");
        }

        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> UpdateBook([FromRoute] Guid id, BookVM item)
        //{
        //    var book = await _dbContext.Books.FindAsync(id);
        //    if(book != null)
        //    {
        //        book.Name = item.Name;
        //        book.Type = item.Type;
        //        book.PublishDate = item.PublishDate;
        //        book.Price = item.Price;

        //        await _dbContext.SaveChangesAsync();
        //        return Ok(book);
        //    }
        //    return NotFound(); 
        //}

        //[HttpDelete]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        //{
        //    var book = await _dbContext.Books.FindAsync(id);
        //    if (book != null)
        //    {
        //        _dbContext.Remove(book);
        //        await _dbContext.SaveChangesAsync();
        //        return Ok(book);
        //    }
        //    return NotFound();
        //}
    }
}
