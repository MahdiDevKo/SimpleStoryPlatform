using MediatR;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Commands
{
    public class AdminBanUserCommandHandler : IRequestHandler<AdminBanUserCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserToken _currentUser;
        public AdminBanUserCommandHandler(
            IUserRepository userRepository,
            ICurrentUserToken currentUserToken
            )
        {
            _userRepo = userRepository;
            _currentUser = currentUserToken;
        }

        public async Task<BaseResponse> Handle(AdminBanUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            #region null checks

            if( request.userGuid == _currentUser.UserGuid) { response.Message = "are you idiot or what? \nAre you trying to ban yourself???"; return response; }

            var user = await _userRepo.GetByGuidAsync(request.userGuid);

            if (user == null) { response.Message = "target user not found"; return response; } 

            if( user.IsBan) { response.Message = "the user is already BAN!"; return response; }

            if( user.IsDeleted) { response.Message = ""; return response; }

            #endregion

            user.IsBan = true;
            user.UnBanDate = request.UnbanDate;
            user.BanReason= request.BanReason;

            await _userRepo.UpdateStatesAsync(user);

            //need to handle cookies in blazor (front side)
            return response;
        }
    }
}
