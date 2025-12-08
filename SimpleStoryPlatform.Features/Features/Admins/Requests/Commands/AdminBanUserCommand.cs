using MediatR;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Requests.Commands
{
    public class AdminBanUserCommand : IRequest<BaseResponse>
    {
        public Guid userGuid { get; set; }
        public string? BanReason { get; set; }
        public DateTime UnbanDate { get; set; }
    }
}
