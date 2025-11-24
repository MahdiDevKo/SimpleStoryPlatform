using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Requests.Commands
{
    public class UserCreateStoryReviewCommand : IRequest<BaseResponse>
    {
        public StoryReviewCreateDto createReviewDto { get; set; }
    }
}
