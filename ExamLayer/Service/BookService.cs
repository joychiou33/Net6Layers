using AutoMapper;
using ExamLayer.Models;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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
      
        public async Task<PagingSearchOutput<BookDto>> GetAllAsync(BookGetAllInput input)
        {
            try
            {
                var data  = await _repository.GetAllAsync();
                var data_dto = _mapper.Map<List<BookDto>>(data).AsQueryable();
                var data_paged = new PagingSearchOutput<BookDto>(data_dto, input.Page, input.PageSize);
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
                var data_dto = _mapper.Map<BookDto>(data);
                return data_dto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CreateAsync(BookInput input)
        {
            try
            {
                var data = _mapper.Map<Book>(input);
                var data_dto = await _repository.CreateAsync(data);
                return data_dto;
            }
            catch
            {
                throw;
            }
        }

        //public async Task<bool> UpdateAsync(Guid id, BookInput input)
        //{
        //    bool retValue = true;
        //    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        try
        //        {
        //            transactionScope.Complete();

        //            return retValue; // 返回更新的记录数
        //        }
        //        catch (Exception ex)
        //        {
        //            // 处理异常，可能需要记录日志或采取其他措施
        //            // 回滚事务
        //            transactionScope.Dispose();

        //            throw ex;
        //        }
        //    }
        //}

        //public bool Delete(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
