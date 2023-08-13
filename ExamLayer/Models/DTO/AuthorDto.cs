namespace ExamLayer.Models.DTO
{
    public class AuthorDto
    { 
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
