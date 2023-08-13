using ExamLayer.Data;
using ExamLayer.Models;
using ExamLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private readonly BookStoreDbContext _dbContext;
        
        public AuthorController(BookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            //Get Data from DB - Domain models
            var authorsDomain = await _dbContext.Authors.ToListAsync();
            
            //Map Domain models to DTOs
            var authorsDto = new List<AuthorDto>();
            foreach (var authorDomain in authorsDomain)
            {
                authorsDto.Add(new AuthorDto()
                {
                    Name = authorDomain.Name,
                    Type = authorDomain.Type,
                    BirthDate = authorDomain.BirthDate,
                    ShortBio = authorDomain.ShortBio
                });
            }

            //Return DTOs
            return Ok(authorsDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] Guid id)
        {
            var authorsDomain = await _dbContext.Authors.FindAsync(id);
            if (authorsDomain != null)
            {
                var authorsDto = new AuthorDto
                {
                    Name = authorsDomain.Name,
                    Type = authorsDomain.Type,
                    BirthDate = authorsDomain.BirthDate,
                    ShortBio = authorsDomain.ShortBio
                };
                return Ok(authorsDto);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Type = authorDto.Type,
                BirthDate = authorDto.BirthDate,
                ShortBio = authorDto.ShortBio
            };
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            var authorsDto = new AuthorDto
            {
                Name = author.Name,
                Type = author.Type,
                BirthDate = author.BirthDate,
                ShortBio = author.ShortBio
            };
            // 201
            //return CreatedAtAction(nameof(GetAuthorById), new { id = authorsDto.Id }, authorsDto);
            // 200
            return Ok(authorsDto);
        }
    }
}
