using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Commands
{
    public class StoryUpdateCommand : IRequest<BaseResponseWithData<StoryDetailsDto>>
    {
        public Guid userGuid { get; set; }
        public bool Publish { get; set; }
        public StoryDetailsDto storyDto { get; set; }
    }
}
