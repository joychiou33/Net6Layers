using ExamLayer.Data;
using ExamLayer.Models;
using ExamLayer.Repositories.Interface;

namespace ExamLayer.Repositories.Service
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _dbContext;

        public BookRepository(BookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Book> CreateAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
