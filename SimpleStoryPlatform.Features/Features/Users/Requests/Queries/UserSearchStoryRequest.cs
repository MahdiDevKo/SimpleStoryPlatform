using MediatR;
using SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs;
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
    public class UserSearchStoryRequest : IRequest<PageResponse<StoryPreviewDto>>
    {
        public SearchRequest<StorySearchOptionsDto> info { get; set; } = new SearchRequest<StorySearchOptionsDto>();
    }
}
