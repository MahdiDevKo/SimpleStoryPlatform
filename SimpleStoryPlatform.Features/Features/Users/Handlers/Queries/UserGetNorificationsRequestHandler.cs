using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserGetNorificationsRequestHandler : IRequestHandler<UserGetNorificationsRequest, PageResponse<NotificationDto>>
    {
        private readonly ICurrentUserToken _currentUser;
        private readonly INotificationRepository _notifRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserGetNorificationsRequestHandler(
            ICurrentUserToken currentUserToken,
            INotificationRepository notificationRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _currentUser = currentUserToken;
            _notifRepo = notificationRepository;
            _userRepo = userRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<NotificationDto>> Handle(UserGetNorificationsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<NotificationDto>();
            int userId = await _userRepo.GetIdByGuid(_currentUser.UserGuid);

            if (userId == 0) { response.Message = "there was a problem with your identity"; return response; }

            var query = _notifRepo.GetQueryable()
                .Where(w => w.UserId == userId);

            var repoRes = await _notifRepo.GetPageAsync(request.req, query);

            response = _mapper.Map<PageResponse<NotificationDto>>(repoRes);

            return response;
        }
    }
}
