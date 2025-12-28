using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Queries
{
    public class GetStoryDetailsRequestHandler : IRequestHandler<GetStoryDetailsRequest, BaseResponseWithData<StoryDetailsDto>>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserToken _currentUser;
        public GetStoryDetailsRequestHandler(IMapper mapper, IStoryRepository storyRepository, ICurrentUserToken currentUser)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _currentUser = currentUser;
        }

        public async Task<BaseResponseWithData<StoryDetailsDto>> Handle(GetStoryDetailsRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryDetailsDto>();

            var story = await _storyRepo.GetStoryDetails(request.storyGuid);

            if (story == null)
                response.Message = "story not found";

            else if (story.CreatedBy != _currentUser.UserGuid)
                response.Message = "you aren't the owner of this story";

            else if (story.IsVisible)
                response.Message = "you cant update a published story";

            else
            {
                response.data = _mapper.Map<StoryDetailsDto>(story);
                response.Success = true;
            }


            return response;
        }
    }
}
