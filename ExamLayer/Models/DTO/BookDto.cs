using ExamLayer.Models.Entity;
using static ExamLayer.Enums.ApiEnum;

namespace ExamLayer.Models.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public float Price { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
