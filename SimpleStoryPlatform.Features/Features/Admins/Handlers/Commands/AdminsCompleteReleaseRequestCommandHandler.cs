using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Commands
{
    public class AdminsCompleteReleaseRequestCommandHandler : IRequestHandler<AdminsCompleteReportCommand<StoryReleaseRequestDetailsDto>, BaseResponse>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IUserRepository _userRepo;
        private readonly INotificationRepository _notifRepo;
        private readonly IStoryReleaseRepository _storyReleaseRepo;
        private readonly IMapper _mapper;
        public AdminsCompleteReleaseRequestCommandHandler(
            IStoryRepository storyRepository,
            IUserRepository userRepository,
            INotificationRepository notification,
            IStoryReleaseRepository storyReleaseRepository,
            IMapper mapper)
        {
            _userRepo = userRepository;
            _storyRepo = storyRepository;
            _notifRepo = notification;
            _storyReleaseRepo = storyReleaseRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(AdminsCompleteReportCommand<StoryReleaseRequestDetailsDto> request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var report = await _storyReleaseRepo.GetReportWithDetails(request.reportInfo.ReportGuid);

            if (report == null) { response.Message = "گذارش موردنظر یافت نشد."; return response; }

            request.reportInfo.report = _mapper.Map<StoryReleaseRequestDetailsDto>(report);

            var writer = await _userRepo.GetByGuidAsync(request.reportInfo.report.CreatedBy);

            var releaseRequest = await _storyReleaseRepo.GetByGuidAsync(request.reportInfo.report.PublicId);

            var story = await _storyRepo.GetByGuidAsync(request.reportInfo.report.Report.StoryGuid);    //report = StoryReleaseRequest --- Report = StoryReport

            //null check
            if (writer == null || releaseRequest == null || story == null) { response.Message = "مشکلی در اعتبار سنجی درخواست شما پیش اومده."; return response; }

            //noficiation to the writer
            Notification notif = new Notification()
            {
                Subject = "نتیجه درخواست انتشار داستان",
                UserId = writer.Id,
            };

            //if (Request Acceptied) story.strike = false
            if (request.reportInfo.IsAccepted)
            {
                story.IsStriked = false;

                await _storyRepo.UpdateStatesAsync(story);

                notif.Text = $"نویسنده ی محترم، با درخواست شما برای انتشار مجدد داستان ({story.Name}) موافقت شد و اکنون میتوانید داستان خودرا منتشر کنید.";
            }
            else
                notif.Text = $"نویسنده ی محترم، با درخواست شما برای انتشار مجدد داستان ({story.Name}) موافقت نشد.";

            if (!string.IsNullOrEmpty(request.reportInfo.SpicialMessage))
                notif.Text += $"\n پیغام ادمین: {request.reportInfo.SpicialMessage}";

            //complete the request
            releaseRequest.IsComplete = true;

            await _storyReleaseRepo.UpdateStatesAsync(releaseRequest);

            //send notification
            await _notifRepo.AddAsync(notif);

            response.Success = true;
            response.Message = "رای شما با موفقیت ثبت شد.";

            return response;
        }
    }
}
