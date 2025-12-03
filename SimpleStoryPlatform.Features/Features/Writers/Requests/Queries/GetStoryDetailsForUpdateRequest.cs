using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Requests.Queries
{
    public class GetStoryDetailsForUpdateRequest : IRequest<BaseResponseWithData<StoryUpdateDto?>>
    {
        public Guid storyGuid { get; set; }
    }
}
