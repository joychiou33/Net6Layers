using ExamLayer.Models.Entity;

namespace ExamLayer.Models.Parameter
{
    public class BookParameter
    {
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
