using ExamLayer.Models.Entity;
using static ExamLayer.Enums.ApiEnum;
namespace ExamLayer.Models.DTO
{
    public class BookInput
    {
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
