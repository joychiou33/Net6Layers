using AutoMapper;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;

namespace ExamLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Book, BookDto>().ReverseMap(); 
            CreateMap<BookInput, Book>().ReverseMap(); ;

        }    
    }
}
