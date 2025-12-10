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
        IMapper _mapper;
        public StoryUpdateCommandHandler(IMapper mapper, IStoryRepository storyRepository, IUserRepository userRepo)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponseWithData<StoryUpdateDto>> Handle(StoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryUpdateDto>();

            var story = await _storyRepo.GetStoryWithSections(request.storyDto.PublicId);

            if (story == null)
                response.Message = "cant found story";

            else if (story.CreatedBy != request.userGuid)
                response.Message = "you dont have the premission to update this story";

            else if (story.IsVisible)
                response.Message = "you cant update PUBLISHED story";

            else
            {
                await _storyRepo.UpdateEntityAsync(_mapper.Map<Story>(request.storyDto));

                var neoStory = _mapper.Map<Story>(request.storyDto);

                story = await _storyRepo.UpdateEntityAsync(story);

                response.Success = true;
                response.Message = "your story has been saved successfully :D!";
                response.data = _mapper.Map<StoryUpdateDto>(neoStory);
            }

            return response;
        }
    }
}
