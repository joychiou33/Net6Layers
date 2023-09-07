namespace ExamLayer.Models
{
    public class BaseSearchOutput<T>
    {
        public int TotalCount { get; set; }
        public List<T>? Data { get; set; }
    }
}
