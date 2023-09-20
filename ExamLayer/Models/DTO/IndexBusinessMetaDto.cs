namespace ExamLayer.Models.DTO
{
    public class IndexBusinessMetaDto
    {
        public int IndexCode { get; set; }
        public string? IndexName { get; set; }
        public string? IndexName_EN { get; set; }
        public string? IndexPurpose { get; set; }
        public string? IndexDefinition { get; set; }
        public string? IndexUnitCode { get; set; }
        public string? IndexUpdateFrequency { get; set; }
        public string? BGCode { get; set; }
        public string? BGName { get; set; }
        public string? LocationCode { get; set; }
        public string? LocationName { get; set; }
        public string? DataOwnerDeptCode { get; set; }
        public string? DataOwnerDeptName { get; set; }
        public string? DataOwnerDeptLevelCode { get; set; }
        public string? DataOwnerDeptLevel { get; set; }
        public string? DataOwnerID { get; set; }
        public string? DataOwnerName { get; set; }
        public string? DataOwnerContactID { get; set; }
        public string? DataSourceID { get; set; }
        public char Sensitivity { get; set; }
        public string? MainCategoryCode { get; set; }
        public string? SubCategoryCode { get; set; }
        public string? MainCategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? APIM_ApiName { get; set; }
        public string? APIM_OpertationName { get; set; }
        public string? APIM_ApiTag { get; set; }
        public string? APIM_Url { get; set; }
        public DateTime APIM_OnlineDate { get; set; }
        public DateTime API_LastPublishTime { get; set; }
        public string? API_FilterDateColumn { get; set; }
        public DateTime API_StartDate { get; set; }
        public string? DMName { get; set; }
        public DateTime LastValidateTime { get; set; }
        public string? LastValidateResult { get; set; }
    }
}
