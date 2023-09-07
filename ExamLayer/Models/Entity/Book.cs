using static ExamLayer.Enums.ApiEnum;

namespace ExamLayer.Models.Entity
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public float Price { get; set; }
        public string? PublishCorp { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
    

}
