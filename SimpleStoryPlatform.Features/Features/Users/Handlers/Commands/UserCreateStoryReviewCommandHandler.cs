using AutoMapper;
using MediatR;
using MediatR.Wrappers;
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
    public class UserCreateStoryReviewCommandHandler : IRequestHandler<UserCreateStoryReviewCommand, BaseResponse>
    {
        IUserRepository _userRepo;
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public UserCreateStoryReviewCommandHandler(IMapper mapper, IUserRepository userRepository, IStoryRepository storyRepo)
        {
            _mapper = mapper;
            _userRepo = userRepository;
            _storyRepo = storyRepo;
        }
        public async Task<BaseResponse> Handle(UserCreateStoryReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.createReviewDto.StoryGuid);

            var user = await _userRepo.GetByGuidAsync(request.createReviewDto.UserGuid);

            if (story != null)
            {
                if (user != null)
                {
                    var review = new StoryReview()
                    {
                        Score = request.createReviewDto.Score,
                        Data = request.createReviewDto.Data,
                        StoryId = story.Id,
                        ReviewerId = user.Id,
                    };

                    var error = await _storyRepo.AddStoryReview(review);

                    if (string.IsNullOrEmpty(error))
                    {
                        response.Success = true;
                        response.Message = "نظر شما با موفقیت ثبت شد.";
                    }
                    else
                        response.Message = error;
                }
                else
                    response.Message = "مشکلی در اعتبار سنجی حساب کاربری شما پیش اومده!";
            }
            else
                response.Message = "داستان موردنظر یافت نشد.";

            return response;
        }
    }
}
