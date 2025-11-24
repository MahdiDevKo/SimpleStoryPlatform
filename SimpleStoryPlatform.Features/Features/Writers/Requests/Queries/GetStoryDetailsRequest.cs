using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Queries
{
    public class GetStoryDetailsRequest : IRequest<BaseResponseWithData<StoryDetailsDto>>
    {
        public Guid userGuid { get; set; }
        public Guid storyGuid { get; set; }
    }
}
