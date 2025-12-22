using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserIsHaveNewWarningRequestHandler : IRequestHandler<UserIsHaveNewWarningRequest, BaseResponseWithData<bool>>
    {
        private readonly IWarningRepository _warningRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserToken _currentUser;
        public UserIsHaveNewWarningRequestHandler(IWarningRepository warningRepository, IUserRepository userRepository, ICurrentUserToken currentUserToken)
        {
            _warningRepo = warningRepository;
            _userRepo = userRepository;
            _currentUser = currentUserToken;
        }
        public async Task<BaseResponseWithData<bool>> Handle(UserIsHaveNewWarningRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<bool>();

            var userId = await _userRepo.GetIdByGuid(_currentUser.UserGuid);

            if(userId == 0) { response.Message = "user not found"; return response; }

            var req = new BaseRequest() { PageNumber = 1, PageSize = 10};

            var query = _warningRepo.GetQueryable()
                .Where(w => w.UserId == userId);

            var pageRes = await _warningRepo.GetPageAsync(req, query);

            response.data = pageRes.Items?.Any(w => !w.IsReaded) ?? false;

            response.Success = true;
            return response;
        }
    }
}
