using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserLoginRequestHandler : IRequestHandler<UserLoginRequest, BaseResponseWithData<User>>
    {
        IUserRepository _userRepo;
        IMapper _mapper;
        public UserLoginRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponseWithData<User>> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<User>();

            var user = await _userRepo.GetByUsername(request.loginDto.Username);

            if (user != null)
            {
                if (user.Password == request.loginDto.Password)
                {
                    response.Success = true;
                    response.Message = "با موفقیت وارد شدید!";
                    response.data = user;
                }
                else
                    response.Message = "رمز عبور اشتباه میباشد.";
            }
            else
                response.Message = "کاربر موردنظر یافت نشد.";

            return response;
        }
    }
}
