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
    public class UserGetWritedStoriesRequestHandler : IRequestHandler<UserGetWritedStoriesRequest, PageResponse<StoryPreviewDto>>
    {
        private readonly IUserRepository _userRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UserGetWritedStoriesRequestHandler(IUserRepository userRepository, IMapper mapper, IStoryRepository storyRepo, IMediator mediator)
        {
            _userRepo = userRepository;
            _mapper = mapper;
            _storyRepo = storyRepo;
            _mediator = mediator;
        }
        public async Task<PageResponse<StoryPreviewDto>> Handle(UserGetWritedStoriesRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryPreviewDto>();

            int userId = await _userRepo.GetIdByGuid(request.userGuid);

            if(userId == 0) { response.Message = "cant find target user."; return response; }

            if (request.PageReq == null)
                request.PageReq = new BaseRequest() { PageNumber = 1, PageSize = 10 };

            var query = _storyRepo.GetQueryable()
                .Where(s => s.WriterId == userId);

            var pageRes = await _storyRepo.GetPageAsync(request.PageReq, query);

            response = _mapper.Map<PageResponse<StoryPreviewDto>>(pageRes);

            response.Success = true;

            return response;
        }
    }
}
