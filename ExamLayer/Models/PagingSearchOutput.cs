namespace ExamLayer.Models
{
    public class PagingSearchOutput<T> : BaseSearchOutput<T>
    {
        public int PageSize { get; set; } 
        public int Page { get; set; } 
        public int TotalPage =>
            (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
        
        public PagingSearchOutput(IQueryable<T> source, int page, int pageSize)
        {
            this.TotalCount = source.Count();
            this.Page = page;
            this.PageSize = pageSize;
            this.Data = source
                         .Skip((page - 1) * (pageSize))
                         .Take(pageSize).ToList();

        }
    }
}
