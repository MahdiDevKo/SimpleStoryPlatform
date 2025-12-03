using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Queries
{
    public class GetStoryDetailsForUpdateRequestHandler : IRequestHandler<GetStoryDetailsForUpdateRequest, BaseResponseWithData<StoryUpdateDto?>>
    {
        IStoryRepository _storyRepo;
        ICurrentUserToken _currentUser;
        IMapper _mapper;
        public GetStoryDetailsForUpdateRequestHandler(IMapper mapper, IStoryRepository storyRepository, ICurrentUserToken currentUser)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _currentUser = currentUser;
        }

        public async Task<BaseResponseWithData<StoryUpdateDto?>> Handle(GetStoryDetailsForUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryUpdateDto?>();

            if(_currentUser.UserGuid == null) { response.Message = "you are not loged in..."; return response; }

            var story = await _storyRepo.GetStoryDetails(request.storyGuid);

            if(_currentUser.UserGuid != story.CreatedBy) { response.Message = "you are not the owner of the story"; return response; }

            response.Success = true;
            response.data = _mapper.Map<StoryUpdateDto>(story);

            return response;
        }
    }
}
