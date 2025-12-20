using MediatR;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Requests.Queries
{
    public class UserReadNotificationRequest : IRequest<BaseResponse>
    {
        public Guid notifGuid { get; set; }
    }
}
