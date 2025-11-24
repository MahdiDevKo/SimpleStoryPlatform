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
    public class StoryUnpublishCommandHandler : IRequestHandler<StoryUnpublishCommand, BaseResponse>
    {
        private readonly IStoryRepository _storyRepo;
        public StoryUnpublishCommandHandler(IStoryRepository storyRepository)
        {
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponse> Handle(StoryUnpublishCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.StoryGuid);

            if (story != null)
            {
                if (story.CreatedBy == request.UserGuid)
                {
                    if (!story.IsPublished) { response.Message = "درحال حاضر، داستان شما منتشر نشده."; return response; }

                    story.IsPublished = false;
                    await _storyRepo.UpdateStatesAsync(story);
                    response.Success = true;
                    response.Message = "داستان شما از حالت انتشار خارج شد!";
                }
                else
                    response.Message = "شما مالک این داستان نیستید!";
            }
            else
                response.Message = "داستان موردنظر یافت نشد.";

            return response;
        }
    }
}
