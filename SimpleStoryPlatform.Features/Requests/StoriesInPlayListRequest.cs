using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Requests
{
    public class StoriesInPlayListRequest : BaseRequest
    {
        public Guid PlayListGuid { get; set; }
    }
}
