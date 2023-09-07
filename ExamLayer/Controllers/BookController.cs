using ExamLayer.Data;
using ExamLayer.Enums;
using ExamLayer.Exceptions;
using ExamLayer.Models;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : BaseController
    {
        //private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BookGetAllInput input)
        {
            var result = new BaseOutput<PagingSearchOutput<BookDto>>();
            result.StatusCode = ApiEnum.ErrorCode.GetSuccess;

            var data_paged = await _bookService.GetAllAsync(input);
            if (data_paged.TotalCount > 0)
            {
                result.Data = data_paged;
            }
            
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = new BaseOutput<BookDto>();
            result.StatusCode = ApiEnum.ErrorCode.GetSuccess;
            
            var data_dto = await _bookService.GetAsync(id);
            if (data_dto == null)
            {
                result.StatusCode = ApiEnum.ErrorCode.DataNotFound;
            }          
            else
            {
                result.Data = data_dto;
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookInput input)
        {
            var result = new BaseOutput<string>();
            result.StatusCode = ApiEnum.ErrorCode.CreateSuccess;
            
            var data_dto = await _bookService.CreateAsync(input);

            if(data_dto < 1) 
            {
                result.StatusCode = ApiEnum.ErrorCode.DataExist;
            }
            return Ok(result);
        }

        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookInput input)
        //{
            
        //    var result = new BaseOutput<string>();

        //    if (!input.Any())
        //        result.StatusCode = ApiEnum.ErrorCode.InvalidParam;

        //    var data_dto = _bookService.UpdateAsync(id, input);
        //    result.StatusCode = ApiEnum.ErrorCode.UpdateSuccess;

        //    if (data_dto < 1)
        //        result.StatusCode = ApiEnum.ErrorCode.DataNotFound;

        //    return Ok(result);
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
