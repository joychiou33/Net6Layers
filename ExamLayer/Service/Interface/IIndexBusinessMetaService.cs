using ExamLayer.Models;
using ExamLayer.Models.DTO;


namespace ExamLayer.Service.Interface
{
    public interface IIndexBusinessMetaService
    {
        //Task <IQueryable<BookDto>> GetAllAsync();
        Task<PagingSearchOutput<IndexBusinessMetaDto>> GetAllAsync(IndexBusinessMetaGetAllInput input);

        Task<IndexBusinessMetaDto> GetAsync(int id);

        //Task<int> CreateAsync(BookInput input);

        //Task<int> UpdateAsync(Guid id, BookInput input);

        //bool Delete(Guid id);
    }
}
