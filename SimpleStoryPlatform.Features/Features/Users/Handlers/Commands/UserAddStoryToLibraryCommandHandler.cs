using MediatR;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Commands
{
    public class UserAddStoryToLibraryCommandHandler : IRequestHandler<UserAddStoryToLibraryCommand, BaseResponse>
    {
        private readonly ICurrentUserToken _currentUser;
        private readonly IUserRepository _userRepo;
        private readonly IStoryRepository _storyRepo;
        public UserAddStoryToLibraryCommandHandler(
            ICurrentUserToken currentUserToken,
            IUserRepository userRepository,
            IStoryRepository storyRepository)
        {
            _currentUser = currentUserToken;
            _userRepo = userRepository;
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponse> Handle(UserAddStoryToLibraryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            #region situation checks

            if (_currentUser.UserGuid == null) { response.Message = "you are NOT Logen in..."; return response; }

            var story = await _storyRepo.GetByGuidAsync(request.storyGuid);

            if (story == null) { response.Message = "cant find your desired story..."; return response; }

            var userLibrary = await _userRepo.GetLibraryAsync(_currentUser.UserGuid);

            if (userLibrary.Contains(request.storyGuid)) { response.Message = "you already have this story in your library"; return response; }

            #endregion

            var success = await _userRepo.AddToLibraryAsycn((Guid)_currentUser.UserGuid, request.storyGuid);

            //idk but i checked it twice :D
            if (success)
            {
                response.Success = true;
                response.Message = "the story added to your library!";
            }
            else
                response.Message = "there was and error in adding story to your library (your user cant find...)";

            return response;
        }
    }
}
