using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Commands
{
    public class UserReportReviewCommandHandler : IRequestHandler<UserReportReviewCommand, BaseResponse>
    {
        IReviewReportRepository _reviewReportRepo;
        IStoryReviewRepository _storyReviewRepo;
        IUserRepository _userRepo;
        ICurrentUserToken _currentUser;
        public UserReportReviewCommandHandler(IStoryReviewRepository storyReviewRepository,
            IReviewReportRepository reviewReportRepository,
            IUserRepository userRepo,
            ICurrentUserToken currentUser)
        {
            _reviewReportRepo = reviewReportRepository;
            _storyReviewRepo = storyReviewRepository;
            _userRepo = userRepo;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse> Handle(UserReportReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var reporterId = await _userRepo.GetIdByGuid(_currentUser.UserGuid);

            var review = await _storyReviewRepo.GetByGuidAsync(request.reportDto.ObjectGuid);

            if (review == null) { response.Message = "The desired comment was not found."; return response; }

            var report = new StoryReviewReport()
            {
                ReviewId = review.Id,
                TargetUserId = await _userRepo.GetIdByGuid(review.CreatedBy),
                ReportText = request.reportDto.Text
            };

            report = await _reviewReportRepo.AddAsync(report);

            response.Success = true;
            response.Message = "Your report was successfully submitted. The result can be viewed in the Notifications section.";

            return response;
        }
    }
}
