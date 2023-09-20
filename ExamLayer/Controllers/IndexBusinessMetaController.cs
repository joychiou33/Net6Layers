using ExamLayer.Enums;
using ExamLayer.Models.DTO;
using ExamLayer.Models;
using ExamLayer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExamLayer.Controllers
{
    public class IndexBusinessMetaController : BaseController
    {
        private readonly IIndexBusinessMetaService _IndexBusinessMetaService;
        public IndexBusinessMetaController(IIndexBusinessMetaService IndexBusinessMetaService)
        {
            this._IndexBusinessMetaService = IndexBusinessMetaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] IndexBusinessMetaGetAllInput input)
        {
            var result = new BaseOutput<PagingSearchOutput<IndexBusinessMetaDto>>();
            result.StatusCode = ApiEnum.ErrorCode.GetSuccess;

            var data_paged = await _IndexBusinessMetaService.GetAllAsync(input);
            if (data_paged.TotalCount > 0)
            {
                result.Data = data_paged;
            }

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = new BaseOutput<IndexBusinessMetaDto>();
            result.StatusCode = ApiEnum.ErrorCode.GetSuccess;

            var data_dto = await _IndexBusinessMetaService.GetAsync(id);
            if (data_dto == null)
            {
                result.StatusCode = ApiEnum.ErrorCode.DataNotFound;
            }
            else
            {
                result.Data = data_dto;
            }
            return Ok(result);
        }
    }
}
