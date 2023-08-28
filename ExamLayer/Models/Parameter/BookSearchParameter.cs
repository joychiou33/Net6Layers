using ExamLayer.Models.Entity;

namespace ExamLayer.Models.Parameter
{
    public class BookSearchParameter
    {
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime? LastPublishDate { get; set; }
        public DateTime? FirstPublishDate { get; set; }

        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
    }
}
