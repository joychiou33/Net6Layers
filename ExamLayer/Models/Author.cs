namespace ExamLayer.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
