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

        public async Task<(List<BookDto> items, int totalCount)> GetAllAsync(PaginationFilter filter)
        {
            try
            {
                var (items, totalCount)  = await _repository.GetAllAsync(filter);
                   
                var result = _mapper.Map<List<BookDto>>(items);
                
                return (result, totalCount);
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
