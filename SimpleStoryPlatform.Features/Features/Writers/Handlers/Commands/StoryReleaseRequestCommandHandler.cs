using MediatR;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryReleaseRequestCommandHandler : IRequestHandler<StoryReleaseRequestCommand, BaseResponse>
    {
        private readonly IStoryReleaseRepository _storyReleaseRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly ICurrentUserToken _currentUser;
        private readonly IUserRepository _userRepo;
        public StoryReleaseRequestCommandHandler(
            IStoryReleaseRepository storyReleaseRepository,
            IStoryRepository storyRepository,
            ICurrentUserToken currentUser,
            IUserRepository userRepository)
        {
            _storyReleaseRepo = storyReleaseRepository;
            _storyRepo = storyRepository;
            _currentUser = currentUser; 
            _userRepo = userRepository;
        }
        public async Task<BaseResponse> Handle(StoryReleaseRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.releaseRequestDto.StoryGuid);

            var writer = await _userRepo.GetByGuidAsync(_currentUser.UserGuid);

            #region null checks & errors
            if (story == null) { response.Message = "story not found."; return response; }

            if (writer == null) { response.Message = "There was a problem with authentication."; return response; }

            if (!story.IsStriked) { response.Message = "your story is NOT striked by admin!"; return response; }

            if (story.CreatedBy != writer.PublicId) { response.Message = "you are NOT the owner of this story"; return response; }

            //bool IsThereAnyReleaseRequest = story.ReleaseRequests.Any(r => r.IsComplete == false);
            bool IsThereAnyReleaseRequest = await _storyRepo.IsThereAnyUnreadReleaseRequest(story.PublicId);

            if (IsThereAnyReleaseRequest) { response.Message = "You have an unanswered request. Please wait for a response."; return response; }
            
            #endregion

            var releaseRequest = new StoryReleaseRequest()
            {
                StoryId = story.Id,
                RequestMessage = request.releaseRequestDto.Text
            };

            await _storyReleaseRepo.AddAsync(releaseRequest);

            response.Message = "Your request has been successfully submitted. Please wait for the admin's response.";
            response.Success = true;

            return response;
        }
    }
}
