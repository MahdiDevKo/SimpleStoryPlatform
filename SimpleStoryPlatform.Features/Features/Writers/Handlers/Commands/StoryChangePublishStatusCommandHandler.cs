using MediatR;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryChangePublishStatusCommandHandler : IRequestHandler<StoryChangePublishStatusCommand, BaseResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly ICurrentUserToken _currentUser;
        public StoryChangePublishStatusCommandHandler(IStoryRepository storyRepository, ICurrentUserToken currentUser)
        {
            _storyRepo = storyRepository;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse> Handle(StoryChangePublishStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.StoryGuid);

            if (story == null) { response.Message = "story not found"; return response; }

            if (story.CreatedBy != _currentUser.UserGuid) { response.Message = "you aren't the owner of this story"; return response; }

            story.IsPublished = !story.IsPublished;

            await _storyRepo.UpdateStatesAsync(story);
            response.Success = true;

            if (story.IsPublished)
                response.Message = "Your story Published Successfully";
            else
                response.Message = "Your story Unpublished successfully";

            return response;
        }
    }
}
