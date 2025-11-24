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
    public class GetWritedStoriesRequest : IRequest<BaseResponseWithData<List<StoryPreviewDto>?>>
    {
        public Guid UserGuid { get; set; }
    }
}
