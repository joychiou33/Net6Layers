using ExamLayer.Filter;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using System.Dynamic;

namespace ExamLayer.Service.Interface
{
    public interface IBookService
    {
        //Task <List<BookDto>> GetAllAsync();
        Task<(List<BookDto> items, int totalCount)> GetAllAsync(PaginationFilter filter);

        Task<BookDto> GetAsync(Guid id);

        Task<int> CreateAsync(QueryBookDto info);

        bool Update(Guid id, QueryBookDto info);

        bool Delete(Guid id);
    }
}
