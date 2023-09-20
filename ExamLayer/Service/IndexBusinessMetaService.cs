using AutoMapper;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using ExamLayer.Models;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;

namespace ExamLayer.Service
{
    public class IndexBusinessMetaService : IIndexBusinessMetaService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AZDPS_APIM_INDEX_LIST> _repository;
        public IndexBusinessMetaService(IMapper mapper, IGenericRepository<AZDPS_APIM_INDEX_LIST> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagingSearchOutput<IndexBusinessMetaDto>> GetAllAsync(IndexBusinessMetaGetAllInput input)
        {
            try
            {
                var data = await _repository.GetAllAsync();
                var data_dto = _mapper.Map<List<IndexBusinessMetaDto>>(data).AsQueryable();
                var data_paged = new PagingSearchOutput<IndexBusinessMetaDto>(data_dto, input.Page, input.PageSize);
                return (data_paged);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IndexBusinessMetaDto> GetAsync(int id)
        {
            try
            {
                var data = await _repository.GetAsync(x => x.IndexCode == id);
                var data_dto = _mapper.Map<IndexBusinessMetaDto>(data);
                return data_dto;
            }
            catch
            {
                throw;
            }
        }
    }
}
