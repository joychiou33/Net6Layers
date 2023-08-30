namespace ExamLayer.Filter
{
    public class PaginationFilter
    {
        public int Page { get; set; } = 1;
        /// <summary>
        /// 顯示筆數
        /// </summary>
        public int PageSize { get; set; } = 100;
        /// <summary>
        /// 排序條件
        /// </summary>
        public string Sort { get; set; } = string.Empty;
        /// <summary>
        /// 篩選條件
        /// </summary>
        public string Filter { get; set; } = string.Empty;


        public PaginationFilter() {
            this.Page = 1;
            this.PageSize = 1000;
        }
        public PaginationFilter(int page, int pageSize)
        {
            this.Page = page < 1 ? 1 : page;
            this.PageSize = pageSize > 1000 ? 1000 : pageSize;
        }

    }
}
