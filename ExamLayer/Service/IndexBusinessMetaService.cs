using AutoMapper;
using ExamLayer.Models;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using ExamLayer.Repositories.Interface;
using ExamLayer.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExamLayer.Service
{
    public class IndexBusinessMetaService : IIndexBusinessMetaService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AZDPS_APIM_INDEX_LIST> _repository;
        public IndexBusinessMetaService(IMapper mapper,IGenericRepository<AZDPS_APIM_INDEX_LIST> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
      
        public async Task<PagingSearchOutput<IndexBusinessMetaDto>> GetAllAsync(IndexBusinessMetaGetAllInput input)
        {
            List<IndexBusinessMetaDto> result = null;
            try
            {
                var data  = await _repository.GetAllAsync();
                //加入搜尋條件
                if (input.IndexCode != null)
                    data = data.Where(x => x.IndexCode == input.IndexCode);
                if (input.IndexName != null)
                    data = data.Where(x => x.IndexName == input.IndexName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.BGName == input.BGName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.LocationName == input.LocationName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.DataOwnerDeptName == input.DataOwnerDeptName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.DataOwnerName == input.DataOwnerName);
                if (input.IndexName != null)
                    data = data.Where(x => x.DataSourceID == input.DataSourceID);
                if (input.IndexCode != null)
                    data = data.Where(x => x.Sensitivity == input.Sensitivity);
                if (input.IndexCode != null)
                    data = data.Where(x => x.MainCategoryName == input.MainCategoryName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.SubCategoryName == input.SubCategoryName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.APIM_ApiName == input.APIM_ApiName);
                if (input.IndexCode != null)
                    data = data.Where(x => x.APIM_OpertationName == input.APIM_OpertationName);
                if (input.IndexName != null)
                    data = data.Where(x => x.APIM_ApiTag == input.APIM_ApiTag);
                if (input.IndexCode != null)
                    data = data.Where(x => x.APIM_OnlineDate == input.APIM_OnlineDate);
                if (input.IndexCode != null)
                    data = data.Where(x => x.DMName == input.DMName);

                //var data_dto = _mapper.Map<List<IndexBusinessMetaDto>>(data).AsQueryable();
                if (data.Any())
                {
                    result = data.Select(x => new IndexBusinessMetaDto
                    {
                        IndexCode = x.IndexCode,
                        IndexName = x.IndexName,
                        IndexPurpose= x.IndexPurpose,
                        IndexDefinition= x.IndexDefinition,
                        IndexUnitCode= x.IndexUnitCode,
                        IndexUpdateFrequency= x.IndexUpdateFrequency,
                        BGName= x.BGName,
                        LocationName= x.LocationName,
                        DataOwnerDeptCode= x.DataOwnerDeptCode,
                        DataOwnerDeptName= x.DataOwnerDeptName,  
                        DataOwnerDeptLevel= x.DataOwnerDeptLevel,   
                        DataOwnerName= x.DataOwnerName,
                        DataOwnerContactID= x.DataOwnerContactID,
                        DataSourceID= x.DataSourceID,
                        Sensitivity = x.Sensitivity,
                        MainCategoryName= x.MainCategoryName,
                        SubCategoryName= x.SubCategoryName,
                        APIM_ApiName= x.APIM_ApiName,
                        APIM_OpertationName =x.APIM_OpertationName,
                        APIM_ApiTag= x.APIM_ApiTag,
                        APIM_Url= x.APIM_Url,
                        APIM_OnlineDate=x.APIM_OnlineDate,
                        API_LastPublishTime =x.API_LastPublishTime,
                        DMName= x.DMName,
                        LastValidateResult= x.LastValidateResult,
                        LastValidateTime= x.LastValidateTime,

                    }).ToList();
                }

                var data_paged = new PagingSearchOutput<IndexBusinessMetaDto>(result, input.Page, input.PageSize);
                return (data_paged);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IndexBusinessMetaDto> GetAsync(int id)
        {
            IndexBusinessMetaDto result = null;
            try
            {
                var data = await _repository.GetAsync(x => x.IndexCode == id);
                //var data_dto = _mapper.Map<IndexBusinessMetaDto>(data);
                if (data != null)
                {
                    result = new IndexBusinessMetaDto
                    {
                        IndexCode = data.IndexCode,
                        IndexName = data.IndexName,
                        IndexPurpose = data.IndexPurpose,
                        IndexDefinition = data.IndexDefinition,
                        IndexUnitCode = data.IndexUnitCode,
                        IndexUpdateFrequency = data.IndexUpdateFrequency,
                        BGName = data.BGName,
                        LocationName = data.LocationName,
                        DataOwnerDeptCode = data.DataOwnerDeptCode,
                        DataOwnerDeptName = data.DataOwnerDeptName,
                        DataOwnerDeptLevel = data.DataOwnerDeptLevel,
                        DataOwnerName = data.DataOwnerName,
                        DataOwnerContactID = data.DataOwnerContactID,
                        DataSourceID = data.DataSourceID,
                        Sensitivity = data.Sensitivity,
                        MainCategoryName = data.MainCategoryName,
                        SubCategoryName = data.SubCategoryName,
                        APIM_ApiName = data.APIM_ApiName,
                        APIM_OpertationName = data.APIM_OpertationName,
                        APIM_ApiTag = data.APIM_ApiTag,
                        APIM_Url = data.APIM_Url,
                        APIM_OnlineDate = data.APIM_OnlineDate,
                        API_LastPublishTime = data.API_LastPublishTime,
                        DMName = data.DMName,
                        LastValidateResult = data.LastValidateResult,
                        LastValidateTime = data.LastValidateTime,
                    };
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        //public async Task<int> CreateAsync(BookInput input)
        //{
        //    try
        //    {
        //        var data = _mapper.Map<Book>(input);
        //        var data_dto = await _repository.CreateAsync(data);
        //        return data_dto;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

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
