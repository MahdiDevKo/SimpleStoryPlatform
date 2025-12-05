using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
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
    public class GetWritedStoriesRequestHandler : IRequestHandler<GetWritedStoriesRequest, PageResponse<StoryPreviewDto>>
    {
        IStoryRepository _storyRepo;
        ICurrentUserToken _currentUser;
        IUserRepository _userRepo;
        IMapper _mapper;
        public GetWritedStoriesRequestHandler(IMapper mapper,
            IStoryRepository storyRepository,
            ICurrentUserToken currentUser,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _currentUser = currentUser;
            _userRepo = userRepository;
        }

        public async Task<PageResponse<StoryPreviewDto>> Handle(GetWritedStoriesRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryPreviewDto>();

            var userGuid = _currentUser.UserGuid;

            if (userGuid == null) { response.Message = "you are NOT loged in"; return response; }

            var query = _storyRepo.GetQueryable();

            query = query.Where(s => s.Writer.CreatedBy == _currentUser.UserGuid);

            var pageReponse = await _storyRepo.GetPageAsync(request.requestProp, query);

            if (pageReponse.Items != null)
                response = _mapper.Map<PageResponse<StoryPreviewDto>>(pageReponse);

            response.Success = true;

            return response;
        }
    }
}
