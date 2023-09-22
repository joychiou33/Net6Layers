using Microsoft.AspNetCore.Mvc;

namespace ExamLayer.Models.DTO
{
    public class IndexBusinessMetaGetAllInput : PagingSearchInput
    {
        [FromQuery(Name = "IndexCode")]
        public int? IndexCode { get; set; }

        [FromQuery(Name = "IndexName")]
        public string? IndexName { get; set; }
        
        [FromQuery(Name = "BGName")]
        public string? BGName { get; set; }

        [FromQuery(Name = "LocationName")]
        public string? LocationName { get; set; }

        [FromQuery(Name = "DataOwnerDeptName")]
        public string? DataOwnerDeptName { get; set; }

        [FromQuery(Name = "DataOwnerName")]
        public string? DataOwnerName { get; set; }

        [FromQuery(Name = "DataSourceID")]
        public string? DataSourceID { get; set; }

        [FromQuery(Name = "Sensitivity")]
        public string? Sensitivity { get; set; }

        [FromQuery(Name = "MainCategoryName")]
        public string? MainCategoryName { get; set; }

        [FromQuery(Name = "SubCategoryName")]
        public string? SubCategoryName { get; set; }

        [FromQuery(Name = "APIM_ApiName")]
        public string? APIM_ApiName { get; set; }

        [FromQuery(Name = "APIM_OpertationName")]
        public string? APIM_OpertationName { get; set; }

        [FromQuery(Name = "APIM_ApiTag")]
        public string? APIM_ApiTag { get; set; }

        [FromQuery(Name = "APIM_OnlineDate")]
        public DateTime APIM_OnlineDate { get; set; }

        [FromQuery(Name = "DMName")]
        public string? DMName { get; set; }

    }
}
