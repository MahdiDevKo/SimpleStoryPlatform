using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Commands
{
    public class UserCreateCommandRequestHandler : IRequestHandler<UserCreateCommandRequest, BaseResponseWithData<User>>
    {
        IUserRepository _userRepo;
        IMapper _mapper;
        public UserCreateCommandRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepo = userRepository;
        }
        public async Task<BaseResponseWithData<User>> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<User>();

            bool Passes = request.createDto.Password == request.createDto.PasswordTwo;
            //need to be fixed with validator
            if (Passes)
            {
                var user = _mapper.Map<User>(request.createDto);

                user.Role = "user";
                user = await _userRepo.AddAsync(user);

                if (user.CreatedAt != null)
                {
                    response.Message = "حساب کاربری شما ایجاد شد.";
                    response.Success = true;
                    response.data = user;
                }
                else
                    response.Message = "مشکلی در ساخت حساب کاربری شما پیش آمده.";
            }
            else
                response.Message = "رمز های عبور باهم یکی نیستند!";

            return response;
        }
    }
}
