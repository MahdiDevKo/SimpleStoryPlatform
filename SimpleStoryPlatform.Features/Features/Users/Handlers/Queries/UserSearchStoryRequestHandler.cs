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
    public class UserSearchStoryRequestHandler : IRequestHandler<UserSearchStoryRequest, BaseResponseWithData<List<StoryPreviewDto>?>>
    {
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public UserSearchStoryRequestHandler(IMapper mapper, IStoryRepository storyRepository)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponseWithData<List<StoryPreviewDto>?>> Handle(UserSearchStoryRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<List<StoryPreviewDto>?>();

            var stories = await _storyRepo.SearchStories(request.searchValue, request.IsAdmin);

            response.Success = true;
            response.data = _mapper.Map<List<StoryPreviewDto>?>(stories);

            return response;
        }
    }
}
