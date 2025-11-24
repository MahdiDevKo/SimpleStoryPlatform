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
    public class UserReportStoryCommandHandler : IRequestHandler<UserReportStoryCommand, BaseResponse>
    {
        IStoryReportRepository _storyReportRepo;
        IStoryRepository _storyRepo;
        IUserRepository _userRepo;
        IMapper _mapper;
        public UserReportStoryCommandHandler(
            IStoryRepository storyRepository,
            IStoryReportRepository storyReportRepository,
            IUserRepository userRepo)
        {
            _storyRepo = storyRepository;
            _storyReportRepo = storyReportRepository;
            _userRepo = userRepo;
        }
        public async Task<BaseResponse> Handle(UserReportStoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.reportDto.ObjectGuid);
            if (story== null) { response.Message = "داستان موردنظر یافت نشد."; return response; }
            if (!story.IsVisible) { response.Message = "داستان موردنظر در دسترس نیست."; return response; }

            var reporterId = await _userRepo.GetIdByGuid(request.reportDto.UserGuid);

            var storyId = await _storyRepo.GetIdByGuid(request.reportDto.ObjectGuid);

            if (reporterId != 0 && storyId != 0)
            {
                
                var report = new StoryReport()
                {
                    StoryId = storyId,
                    TargetUserId = reporterId,
                    ReportText = request.reportDto.Text,
                };

                report = await _storyReportRepo.AddAsync(report);

                response.Success = true;
                response.Message = "گذارش شما با موفقیت ثبت شدو لطفا تا پیگیری آن منتظر بمانید.";
            }
            else
                response.Message = "مشکلی در اعتبار سنجی اطلاعات ورودی رخ داده.";

            return response;
        }
    }
}
