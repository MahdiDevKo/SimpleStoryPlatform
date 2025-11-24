using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Responses
{
    public class BaseResponseWithData<T> : BaseResponse
    {
        public T? data { get; set; }
    }
}
