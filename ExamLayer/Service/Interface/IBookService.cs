using ExamLayer.Models;

namespace ExamLayer.Service.Interface
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks();
        Task<List<Book>> GetBookById(Guid id);

        void Create(Book entity);
    }
}
