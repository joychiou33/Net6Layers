using ExamLayer.Models;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamLayer.Service
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        public BookService(IGenericRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> GetBooks()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Book>> GetBookById(Guid id)
        {
            try
            {
                return await _repository.FindByCondition(x => x.Id == id).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public void Create(Book entity)
        {
            try
            {
               _repository.Create(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
