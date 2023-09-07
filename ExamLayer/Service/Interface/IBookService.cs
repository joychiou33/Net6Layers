using ExamLayer.Models;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using System.Dynamic;

namespace ExamLayer.Service.Interface
{
    public interface IBookService
    {
        //Task <IQueryable<BookDto>> GetAllAsync();
        Task<PagingSearchOutput<BookDto>> GetAllAsync(BookGetAllInput input);

        Task<BookDto> GetAsync(Guid id);

        Task<int> CreateAsync(BookInput input);

        //Task<int> UpdateAsync(Guid id, BookInput input);

        //bool Delete(Guid id);
    }
}
