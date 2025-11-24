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
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Commands
{
    public class AdminsCompleteStoryReportCommandHandler : IRequestHandler<AdminsCompleteReportCommand<StoryReportDto>, BaseResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IUserRepository _userRepo;
        private readonly INotificationRepository _notifRepo;
        private readonly IStoryReportRepository _storyReportRepo;
        private readonly IMapper _mapper;
        public AdminsCompleteStoryReportCommandHandler(
            IStoryRepository storyRepository,
            IUserRepository userRepository,
            INotificationRepository notification,
            IMapper mapper,
            IStoryReportRepository storyReporyRepo)
        {
            _userRepo = userRepository;
            _storyRepo = storyRepository;
            _notifRepo = notification;
            _mapper = mapper;
            _storyReportRepo = storyReporyRepo;
        }

        public async Task<BaseResponse> Handle(AdminsCompleteReportCommand<StoryReportDto> request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse();

            var report = await _storyReportRepo.GetReportWithDetails(request.reportInfo.ReportGuid);

            if(report == null) { response.Message = "گذارش موردنظر یافت نشد."; return response; }
            
            request.reportInfo.report = _mapper.Map<StoryReportDto>(report);

            var reporter = await _userRepo.GetByGuidAsync(request.reportInfo.report.CreatedBy);

            var story = await _storyRepo.GetByGuidAsync(request.reportInfo.report.StoryGuid);

            //close the report
            report.IsComplete = true;

            await _storyReportRepo.UpdateStatesAsync(report);

            if (request.reportInfo.IsAccepted)
            {
                var targetUser = await _userRepo.GetUserWithWarnings(request.reportInfo.TargetUserGuid);

                //strike the story

                story.IsStriked = true;
                story = await _storyRepo.UpdateStatesAsync(story);

                //delete same repororts
                await _storyReportRepo.RemoveCurrentReports(story.Id);

                //add warning
                Warning warning = new Warning()
                {
                    UserId = targetUser.Id,
                    Subject = "تخلف",
                    Reason = "تخلف در محتوای داستان",
                    Details = "بدون جزییات"
                };

                targetUser.Warnings.Add(warning);

                await _userRepo.UpdateEntityAsync(targetUser);

                //send notifications
                Notification notifOne = new Notification()
                {
                    UserId = reporter.Id,
                    Subject = "نتیجه گذارش شما",
                    Text = $"کاربر گرامی، گذارش شما مورد تایید ادمین قرار گرفت و داستان ({story.Name}) تا اطلاع ثانوی، از دسترس خارج شد."
                };

                Notification notifTwo = new Notification()
                {
                    UserId = targetUser.Id,
                    Subject = "شکایطی علیه شما ثبت شد",
                    Text = $"کاربر محترم، داستان شما با نام ({story.Name}) بدلیل نقض قوانین سایت از دسترس خارج شد. درصورت تکرار، احتمال مسدود شدن حساب کاربری شما نیز وجود دارد."
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
                    Text = $"کاربر گرامی، گذارش شما درباره ی داستان ({story.Name}) توسط ادمین رد شد."
                };

                await _notifRepo.AddAsync(notif);
            }


            response.Success = true;
            response.Message = "رای شما با موفقیت ثبت شد.";

            return response;
        }
    }
}
