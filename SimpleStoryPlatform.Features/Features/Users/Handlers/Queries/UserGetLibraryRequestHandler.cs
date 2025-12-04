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
    public class UserGetLibraryRequestHandler : IRequestHandler<UserGetLibraryRequest, BaseResponseWithData<Guid[]?>>
    {
        private readonly ICurrentUserToken _currentUser;
        private readonly IUserRepository _userRepo;
        public UserGetLibraryRequestHandler(
            ICurrentUserToken currentUserToken,
            IUserRepository userRepository)
        {
            _currentUser = currentUserToken;
            _userRepo = userRepository;
        }
        public async Task<BaseResponseWithData<Guid[]?>> Handle(UserGetLibraryRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<Guid[]?>();

            if (_currentUser.UserGuid == null) { response.Message = "you are NOT Logen in..."; return response; }

            var library = await _userRepo.GetLibraryAsync(_currentUser.UserGuid);

            response.data = library;

            response.Success = true;

            return response;
        }
    }
}
