using ExamLayer.Models.Entity;

namespace ExamLayer.Models.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
