using MediatR;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserReadNotificationRequestHandler : IRequestHandler<UserReadNotificationRequest, BaseResponse>
    {
        private readonly INotificationRepository _notifRepo;
        public UserReadNotificationRequestHandler(INotificationRepository notificationRepository)
        {
            _notifRepo = notificationRepository;
        }
        public async Task<BaseResponse> Handle(UserReadNotificationRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var notification = await _notifRepo.GetByGuidAsync(request.notifGuid);

            if (notification == null) { response.Message = "cant find warning! (for set as READED warning)"; return response; }

            notification.IsReaded = true;

            await _notifRepo.UpdateStatesAsync(notification);

            response.Success = true;
            return response;
        }
    }
}
