using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Commands
{
    public class AdminsCompleteReviewReportCommandHandler : IRequestHandler<AdminsCompleteReportCommand<ReviewReportDto>, BaseResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IUserRepository _userRepo;
        private readonly INotificationRepository _notifRepo;
        private readonly IMapper _mapper;
        private readonly IReviewReportRepository _reviewReportRepo;
        public AdminsCompleteReviewReportCommandHandler(
            IStoryRepository storyRepository,
            IUserRepository userRepository,
            INotificationRepository notification,
            IMapper mapper,
            IReviewReportRepository reviewReportRepository)
        {
            _userRepo = userRepository;
            _storyRepo = storyRepository;
            _notifRepo = notification;
            _mapper = mapper;
            _reviewReportRepo = reviewReportRepository;
        }
        public async Task<BaseResponse> Handle(AdminsCompleteReportCommand<ReviewReportDto> request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var report = await _reviewReportRepo.GetReportWithDetails(request.reportInfo.ReportGuid);

            if (report == null) { response.Message = "گزارش موردنظر یافت نشد."; return response; }

            request.reportInfo.report = _mapper.Map<ReviewReportDto>(report);

            var reporter = await _userRepo.GetByGuidAsync(request.reportInfo.report.CreatedBy);

            var review = report.Object;

            if (request.reportInfo.IsAccepted)
            {
                var targetUser = await _userRepo.GetUserWithWarnings(request.reportInfo.report.TargetUser.PublicId);

                //delete the review
                var rawStory = await _storyRepo.GetAsync(review.StoryId);

                if(rawStory == null)
                {
                    response.Message = "داستان موردنظر برای حذف کامنت یافت نشد.";
                    return response;
                }

                var story = await _storyRepo.GetStoryDetails(rawStory.PublicId);
                story.Reviews.FirstOrDefault(r => r.PublicId == review.PublicId).IsDeleted = true;

                 await _storyRepo.UpdateStatesAsync(story);

                //add warning
                Warning warning = new Warning()
                {
                    Subject = "تخلف",
                    UserId = targetUser.Id,
                    Reason = "تخلف در نوشتن دیدگاه",
                    Details = "بدون جزییات"
                };
                targetUser.Warnings.Add(warning);

                await _userRepo.UpdateEntityAsync(targetUser);

                //send notifications
                Notification notifOne = new Notification()
                {
                    UserId = reporter.Id,
                    Subject = "نتیجه گذارش شما",
                    Text = $"کاربر گرامی، گذارش شما مورد تایید ادمین قرار گرفت و نظر کاربر ({targetUser.FirstName} {targetUser.LastName}) از وبسایت حذف شد."
                };

                Notification notifTwo = new Notification()
                {
                    UserId = targetUser.Id,
                    Subject = "شکایطی علیه شما ثبت شد",
                    Text = $"کاربر محترم، نظر شما با محتوای: \n ({review.Data}) \n بدلیل نقض قوانین سایت، حذف شد. درصورت تکرار، احتمال مسدود شدن حساب کاربری شما نیز وجود دارد."
                };

                if (!string.IsNullOrEmpty(request.reportInfo.SpicialMessage))
                    notifTwo.Text += $"\n پیغام ادمین: {request.reportInfo.SpicialMessage}";

                await _notifRepo.AddAsync(notifOne);
                await _notifRepo.AddAsync(notifTwo);


            }
            else
            {
                Notification notif = new Notification()
                {
                    UserId = reporter.Id,
                    Subject = "نتیجه گذارش شما",
                    Text = $"کاربر گرامی، گذارش شما درباره ی نظر({review.Data}) توسط ادمین رد شد."
                };

                await _notifRepo.AddAsync(notif);
            }
            //close the report
            report.IsComplete = true;

            await _reviewReportRepo.UpdateStatesAsync(report);

            response.Success = true;
            response.Message = "رای شما با موفقیت ثبت شد.";

            return response;
        }
    }
}
