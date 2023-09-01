using AutoMapper;
using ExamLayer.Filter;
using ExamLayer.Models;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamLayer.Service
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Book> _repository;
        public BookService(IMapper mapper,IGenericRepository<Book> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
      
        public async Task<PageList<BookDto>> GetAllAsync(PaginationFilter filter)
        {
            try
            {
                var data  = await _repository.GetAllAsync();
                var data_dto = _mapper.Map<List<BookDto>>(data).AsQueryable();
                var data_paged = new PageList<BookDto>(data_dto, filter.Page,filter.PageSize);
                return (data_paged);
            }
            catch
            {
                throw;
            }
        }
        public async Task<BookDto> GetAsync(Guid id)
        {
            try
            {
                var data = await _repository.GetAsync(x => x.Id == id);
                var result = _mapper.Map<BookDto>(data);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CreateAsync(QueryBookDto info)
        {
            try
            {
                var result = _mapper.Map<Book>(info);
                return  await _repository.CreateAsync(result);
            }
            catch
            {
                throw;
            }
        }

        public bool Update(Guid id, QueryBookDto info)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
