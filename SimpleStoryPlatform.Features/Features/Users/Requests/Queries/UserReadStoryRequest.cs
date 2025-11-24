using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Requests.Queries
{
    public class UserReadStoryRequest : IRequest<BaseResponseWithData<StoryDetailsDto?>>
    {
        public Guid storyGuid { get; set; }
        public bool IsAdmin { get; set; }
    }
}
