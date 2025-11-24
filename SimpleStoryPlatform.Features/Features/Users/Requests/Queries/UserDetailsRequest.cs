using MediatR;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Requests.Queries
{
    public class UserDetailsRequest : IRequest<BaseResponseWithData<UserDetailsDto>>
    {
        public Guid userGuid { get; set; }
    }
}
