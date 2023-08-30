using AutoMapper;
using ExamLayer.Models.DTO;
using ExamLayer.Models.Entity;
using ExamLayer.Models.ViewModel;

namespace ExamLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Book, BookDto>().ReverseMap(); 
            CreateMap<QueryBookDto, Book>().ReverseMap(); ;

        }    
    }
}
