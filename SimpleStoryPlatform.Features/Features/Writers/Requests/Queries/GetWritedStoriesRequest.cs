using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Queries
{
    public class GetWritedStoriesRequest : IRequest<PageResponse<StoryPreviewDto>>
    {
        public BaseRequest requestProp { get; set; } = new BaseRequest();
    }
}
