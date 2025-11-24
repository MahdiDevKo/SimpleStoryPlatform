using MediatR;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryReleaseRequestCommandHandler : IRequestHandler<StoryReleaseRequestCommand, BaseResponse>
    {
        private readonly IStoryReleaseRepository _storyReleaseRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly ICurrentUserToken _currentUser;
        private readonly IUserRepository _userRepo;
        public StoryReleaseRequestCommandHandler(
            IStoryReleaseRepository storyReleaseRepository,
            IStoryRepository storyRepository,
            ICurrentUserToken currentUser,
            IUserRepository userRepository)
        {
            _storyReleaseRepo = storyReleaseRepository;
            _storyRepo = storyRepository;
            _currentUser = currentUser; 
            _userRepo = userRepository;
        }
        public async Task<BaseResponse> Handle(StoryReleaseRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetStoryDetails(request.releaseRequestDto.StoryGuid);

            var writer = await _userRepo.GetByGuidAsync( (Guid)_currentUser.UserGuid);

            #region null checks & errors
            if (story == null) { response.Message = "داستان یافت نشد"; return response; }

            if (writer == null) { response.Message = "مشکلی در اعتبارسنجی شما رخ داده"; return response; }

            if (!story.IsStriked) { response.Message = "داستان شما توسط ادمین از دسترس خارج نشده!"; return response; }

            if (story.CreatedBy != writer.PublicId) { response.Message = "شما صاحب این داستان نیستید :/"; return response; }

            bool IsThereAnyReleaseRequest = story.ReleaseRequests.Any(r => r.IsComplete == false);
            if (IsThereAnyReleaseRequest) { response.Message = "درخواست شما قبلا ثبت شده. لطفا تا جواب ادمین صبر کنید"; return response; }
            
            #endregion


            var admin = await _userRepo.GetByGuidAsync(story.Reports.Last().CreatedBy);
            if (admin == null) { response.Message = "مشکلی در دریافت اطلاعات پیش اومده"; return response; }

            var releaseRequest = new StoryReleaseRequest()
            {
                StoryReportId = story.Reports.Last().Id,
                TargetUserId = admin.Id,
                StoryId = story.Id,
                ReportText = request.releaseRequestDto.Text
            };

            await _storyReleaseRepo.AddAsync(releaseRequest);

            response.Message = "درخواست شما با موفقیت ثبت شد. لطفا تا جواب ادمین منتظر بمانید.";
            response.Success = true;

            return response;
        }
    }
}
