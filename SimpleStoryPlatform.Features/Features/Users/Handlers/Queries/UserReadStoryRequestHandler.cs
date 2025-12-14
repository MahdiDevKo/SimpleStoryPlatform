using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
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
    public class UserReadStoryRequestHandler : IRequestHandler<UserReadStoryRequest, BaseResponseWithData<StoryDetailsDto?>>
    {
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public UserReadStoryRequestHandler(IMapper mapper, IStoryRepository storyRepository)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponseWithData<StoryDetailsDto?>> Handle(UserReadStoryRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryDetailsDto?>();

            var story = _mapper.Map<StoryDetailsDto>(await _storyRepo.GetStoryDetails(request.storyGuid));

            if (story == null) { response.Message = "story not found"; return response; }

            if (story.IsVisible || request.IsAdmin)
            {
                response.data = story;
                response.Success = true;
            }
            else
                response.Message = "this story is not accessable";


            return response;
        }
    }
}
