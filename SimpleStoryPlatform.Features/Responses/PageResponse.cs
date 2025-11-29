using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Responses
{
    public class PageResponse<T> : BaseResponse
    {
        public List<T>? Items { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
    }
}
