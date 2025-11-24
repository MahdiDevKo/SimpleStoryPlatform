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
    public class UserSearchStoryRequest : IRequest<BaseResponseWithData<List<StoryPreviewDto>?>>
    {
        public string? searchValue { get; set; }
        public bool IsAdmin { get; set; }
    }
}
