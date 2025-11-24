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
        public UserReportReviewCommandHandler(IStoryReviewRepository storyReviewRepository,
            IReviewReportRepository reviewReportRepository,
            IUserRepository userRepo)
        {
            _reviewReportRepo = reviewReportRepository;
            _storyReviewRepo = storyReviewRepository;
            _userRepo = userRepo;
        }
        public async Task<BaseResponse> Handle(UserReportReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var reporterId = await _userRepo.GetIdByGuid(request.reportDto.UserGuid);

            var reviewId = await _storyReviewRepo.GetIdByGuid(request.reportDto.ObjectGuid);
            
            if (reporterId != 0 && reviewId != 0)
            {
                var report = new StoryReviewReport()
                {
                    ReviewId = reviewId,
                    TargetUserId = reporterId,
                    ReportText = request.reportDto.Text,
                };

                report = await _reviewReportRepo.AddAsync(report);

                response.Success = true;
                response.Message = "گذارش شما با موفقیت ثبت شدو لطفا تا پیگیری آن منتظر بمانید.";
            }
            else
                response.Message = "مشکلی در اعتبار سنجی اطلاعات ورودی رخ داده.";

            return response;
        }
    }
}
