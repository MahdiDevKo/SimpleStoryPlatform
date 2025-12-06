using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserGetLibraryRequestHandler : IRequestHandler<UserGetLibraryRequest, PageResponse<StoryPreviewDto>>
    {
        private readonly ICurrentUserToken _currentUser;
        private readonly IStoryRepository _storyRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public UserGetLibraryRequestHandler(
            ICurrentUserToken currentUserToken,
            IStoryRepository storyRepository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _currentUser = currentUserToken;
            _storyRepo = storyRepository;
            _userRepo = userRepository;
            _mapper = mapper;
        }

        async Task<PageResponse<StoryPreviewDto>> IRequestHandler<UserGetLibraryRequest, PageResponse<StoryPreviewDto>>.Handle(UserGetLibraryRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryPreviewDto>();

            if (_currentUser.UserGuid == null) { response.Message = "you are NOT Logen in..."; return response; }

            if (request.reqProp == null)
                request.reqProp = new BaseRequest();

            var query = _storyRepo.GetQueryable();

            var user = await _userRepo.GetByGuidAsync(_currentUser.UserGuid);

            query = query.Where(s => s.InLibraryOf.Contains(user));

            var repoRes = await _storyRepo.GetPageAsync(request.reqProp, query);

            response = _mapper.Map<PageResponse<StoryPreviewDto>>(repoRes);

            response.Success = true;
            return response;
        }
    }
}
