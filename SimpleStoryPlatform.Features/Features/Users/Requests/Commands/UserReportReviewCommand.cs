using MediatR;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Requests.Commands
{
    public class UserReportReviewCommand : IRequest<BaseResponse>
    {
        public UserReportDto reportDto { get; set; }
    }
}
