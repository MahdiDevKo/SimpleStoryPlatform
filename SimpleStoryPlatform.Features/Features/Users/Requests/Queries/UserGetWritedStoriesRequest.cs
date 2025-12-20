using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Requests.Queries
{
    public class UserGetWritedStoriesRequest : IRequest<PageResponse<StoryPreviewDto>>
    {
        public Guid userGuid { get; set; }
        public BaseRequest? PageReq { get; set; }
    }
}
