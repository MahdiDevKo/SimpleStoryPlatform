using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
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
    public class UserDetailsRequestHandler : IRequestHandler<UserDetailsRequest, BaseResponseWithData<UserDetailsDto>>
    {
        IUserRepository _userRepo;
        IMapper _mapper;
        public UserDetailsRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepo = userRepository;
        }
        public async Task<BaseResponseWithData<UserDetailsDto>> Handle(UserDetailsRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<UserDetailsDto>();

            var user = await _userRepo.GetUserWithAllDetails(request.userGuid);

            if (user == null)
                response.Message = "کاربر موردنظر یافت نشد.";
            else
            {
                response.Success = true;
                response.data = _mapper.Map<UserDetailsDto>(user);
            }

            return response;
        }
    }
}
