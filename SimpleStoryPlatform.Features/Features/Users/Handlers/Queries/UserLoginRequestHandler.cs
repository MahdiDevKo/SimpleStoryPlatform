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

            if (user == null) { response.Message = "user not found."; return response; }

            if (user.Password != request.loginDto.Password) { response.Message = "password incorrect"; return response; }

            if (user.IsBan) { response.Message = $"your account has been BANED!\nBan reason:{user.BanReason} \nUnban date:{user.UnBanDate}"; return response; }

            if (user.IsDeleted) { response.Message = $"your account has been deleted. dont try to access it (or i will be eat your dad)"; return response; }

            response.Success = true;
            response.Message = "";
            response.data = user;

            return response;
        }
    }
}
