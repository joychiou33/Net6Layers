using ExamLayer.Models;

namespace ExamLayer.Filter
{
    public class PageList<T>
    {
        public int Page { get; set; } = 1;
        /// <summary>
        /// 顯示筆數
        /// </summary>
        public int PageSize { get; set; } = 10;

        public int TotalCount { get; set; }
        public int TotalPage =>
           (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
        public List<T> List { get; }
        public PageList(IQueryable<T> source, int page, int pageSize)
        {
           this.TotalCount = source.Count();
           this.Page=page;
           this.PageSize=pageSize;
           this.List= source
                        .Skip((page - 1) * (pageSize))
                        .Take(pageSize).ToList();

        }
    }
}
