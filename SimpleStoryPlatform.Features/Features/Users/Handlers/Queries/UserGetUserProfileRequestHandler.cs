using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
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
    public class UserGetUserProfileRequestHandler : IRequestHandler<UserGetUserProfileRequest, BaseResponseWithData<UserProfileDto>>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UserGetUserProfileRequestHandler(IUserRepository userRepository,  IMapper mapper, IMediator mediator)
        {
            _userRepo = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<BaseResponseWithData<UserProfileDto>> Handle(UserGetUserProfileRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<UserProfileDto>();

            var user = await _userRepo.GetByGuidAsync(request.userGuid);

            if(user == null) { response.Message = "couldn't find user"; return response; }

            response.data = _mapper.Map<UserProfileDto>(user);

            response.Success = true;

            var writedStoriesPageRequest = new UserGetWritedStoriesRequest() { userGuid = request.userGuid };
            
            var writedStoriesPageRes = await _mediator.Send(writedStoriesPageRequest);

            //getting the first page of: user written stories
            if (writedStoriesPageRes.Success)
                response.data.WritedStories = writedStoriesPageRes;
            else
                response.Message = writedStoriesPageRes.Message;

            return response;
        
        }
    }
}
