using ExamLayer.Models.DTO;
using ExamLayer.Models;

namespace ExamLayer.Service.Interface
{
    public interface ISettingPermissionService
    {
        Task<PagingSearchOutput<IndexBusinessMetaDto>> GetAllAsync(IndexBusinessMetaGetAllInput input);

        Task<IndexBusinessMetaDto> GetAsync(int id);
    }
}
