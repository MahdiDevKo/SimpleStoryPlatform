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
        private readonly IUserRepository _userRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly ICurrentUserToken _currentUser;
        public UserCreateStoryReviewCommandHandler(
            IUserRepository userRepository,
            IStoryRepository storyRepo,
            ICurrentUserToken currentUser)
        {
            _userRepo = userRepository;
            _storyRepo = storyRepo;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse> Handle(UserCreateStoryReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.createReviewDto.StoryGuid);

            if (story == null) { response.Message = "your target story cant be find..."; return response; }

            var user = await _userRepo.GetByGuidAsync(_currentUser.UserGuid);

            if (user == null) { response.Message = "there is a problem with your identity (user not found)"; return response; }

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
                response.Message = "your review submitted successfully";
            }
            else
                response.Message = error;


            return response;
        }
    }
}
