using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryPlayListManageCommandHandler : IRequestHandler<StoryPlayListManageCommand, BaseResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IStoryPlayListRepository _storyPlaylistRepo;
        private readonly ICurrentUserToken _currentUser;
        public StoryPlayListManageCommandHandler(IStoryRepository storyRepository, ICurrentUserToken currentUserToken, IStoryPlayListRepository storyPlayListRepository)
        {
            _storyRepo = storyRepository;
            _currentUser = currentUserToken;
            _storyPlaylistRepo = storyPlayListRepository;
        }
        public async Task<BaseResponse> Handle(StoryPlayListManageCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.manageDto.storyGuid);

            if (story == null) { respose.Message = "story not found."; return respose; }

            if (story.CreatedBy != _currentUser.UserGuid) { respose.Message = "you are NOT the owner of this story."; return respose; }


            if (request.manageDto.IsAdd)
            {
                var storyPlayList = await _storyPlaylistRepo.GetByGuidAsync(request.manageDto.playListGuid);

                if (storyPlayList == null) { respose.Message = "play list not found."; return respose; }

                story.PlayListId = storyPlayList.Id;
            }
            else
                story.PlayListId = null;


            await _storyRepo.UpdateStatesAsync(story);

            respose.Success = true;
            respose.Message = "story's playlist play list changed.";

            return respose;
        }
    }
}
