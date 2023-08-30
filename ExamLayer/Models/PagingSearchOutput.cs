namespace ExamLayer.Models
{
    public class PagingSearchOutput<T> : BaseOutput<T>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
