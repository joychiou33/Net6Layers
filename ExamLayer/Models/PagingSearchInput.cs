using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ExamLayer.Models
{
    public class PagingSearchInput
    {
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 1000;

        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;

        public PagingSearchInput()
        {
            this.Page = 1;
            this.PageSize = 1000;
        }
        public PagingSearchInput(int page, int pageSize)
        {
            this.Page = page < 1 ? 1 : page;
            this.PageSize = pageSize > 1000 ? 1000 : pageSize;
        }
    }
}
