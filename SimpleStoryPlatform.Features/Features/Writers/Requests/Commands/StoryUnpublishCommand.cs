using MediatR;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Commands
{
    public class StoryUnpublishCommand : IRequest<BaseResponse>
    {
        public Guid UserGuid { get; set; }
        public Guid StoryGuid { get; set; }
    }
}
