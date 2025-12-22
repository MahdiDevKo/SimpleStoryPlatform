using MediatR;
using MediatR.Pipeline;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Commands
{
    public class StoryPlayListManageCommand : IRequest<BaseResponse>
    {
        public StoryPlayListManageDto manageDto { get; set; }
    }
}
