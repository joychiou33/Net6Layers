using ExamLayer.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using static ExamLayer.Enums.ApiEnum;
using System.Xml.Linq;

namespace ExamLayer.Models.DTO
{
    public class BookGetAllInput : PagingSearchInput
    {
        [FromQuery(Name = "Name")]
        public string? Name { get; set; }
        [FromQuery(Name = "Type")]
        public BookType Type { get; set; }
        [FromQuery(Name = "PublishDate")]
        public DateTime PublishDate { get; set; }
        [FromQuery(Name = "Price")]
        public float Price { get; set; }
    }
}
