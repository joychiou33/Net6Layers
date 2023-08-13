using ExamLayer.Models;

namespace ExamLayer.Repositories.Interface
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book book);

    }
}
