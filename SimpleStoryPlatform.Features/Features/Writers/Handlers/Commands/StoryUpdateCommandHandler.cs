using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryUpdateCommandHandler : IRequestHandler<StoryUpdateCommand, BaseResponseWithData<StoryUpdateDto>>
    {
        IStoryRepository _storyRepo;
        ICurrentUserToken _currentUser;
        IMapper _mapper;
        public StoryUpdateCommandHandler(IMapper mapper, IStoryRepository storyRepository, ICurrentUserToken currentUserToken)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _currentUser = currentUserToken;
        }
        public async Task<BaseResponseWithData<StoryUpdateDto>> Handle(StoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryUpdateDto>();

            var story = await _storyRepo.GetStoryDetails(request.storyDto.PublicId, false);

            if (story == null)
                response.Message = "cant found story";

            else if (story.CreatedBy != _currentUser.UserGuid)
                response.Message = "you dont have the premission to update this story";

            else if (story.IsVisible)
                response.Message = "you cant update PUBLISHED story";

            else
            {
                _mapper.Map(request.storyDto, story);

                story = await _storyRepo.UpdateEntityAsync(story);

                var neoStory = _mapper.Map<StoryUpdateDto>(story);

                response.Success = true;
                response.Message = "your story has been saved successfully :D!";
                response.data = _mapper.Map<StoryUpdateDto>(neoStory);
            }

            return response;
        }
    }
}
