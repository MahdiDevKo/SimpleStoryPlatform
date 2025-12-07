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
        private readonly IStoryReportRepository _storyReportRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserToken _currentUser;
        public UserReportStoryCommandHandler(
            IStoryRepository storyRepository,
            IStoryReportRepository storyReportRepository,
            IUserRepository userRepo,
            ICurrentUserToken currentUser)
        {
            _storyRepo = storyRepository;
            _storyReportRepo = storyReportRepository;
            _userRepo = userRepo;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse> Handle(UserReportStoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetByGuidAsync(request.reportDto.ObjectGuid);

            if (story == null) { response.Message = "The desired story was not found."; return response; }

            if (!story.IsVisible) { response.Message = "your desired story is not available"; return response; }

            var reporterId = await _userRepo.GetIdByGuid(_currentUser.UserGuid);

            var report = new StoryReport()
            {
                StoryId = story.Id,
                TargetUserId = await _userRepo.GetIdByGuid(story.CreatedBy),
                ReportText = request.reportDto.Text,
            };

            report = await _storyReportRepo.AddAsync(report);

            response.Success = true;
            response.Message = "your report has been added to queue. please be patient until admins response.";

            return response;
        }
    }
}
