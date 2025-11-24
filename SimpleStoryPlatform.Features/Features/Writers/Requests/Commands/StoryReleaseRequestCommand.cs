using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Commands
{
    public class StoryReleaseRequestCommand : IRequest<BaseResponse>
    {
        public StoryReleaseRequestCreateDto releaseRequestDto { get; set; }
    }
}
