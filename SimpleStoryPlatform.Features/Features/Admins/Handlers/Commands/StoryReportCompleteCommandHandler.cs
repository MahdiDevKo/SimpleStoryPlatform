using MediatR;
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
    public class StoryReportCompleteCommandHandler : IRequestHandler<StoryReportCompleteCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IStoryReportRepository _storyReportRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly INotificationRepository _notifRepo;
        private readonly ICurrentUserToken _currentUser;
        public StoryReportCompleteCommandHandler(IUserRepository userRepository,
            IStoryReportRepository storyReportRepo,
            IStoryRepository storyRepo,
            INotificationRepository notifRepo,
            ICurrentUserToken current
            )
        {
            _userRepo = userRepository;
            _storyReportRepo = storyReportRepo;
            _storyRepo = storyRepo;
            _notifRepo = notifRepo;
            _currentUser = current;
        }
        public async Task<BaseResponse> Handle(StoryReportCompleteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var report = await _storyReportRepo.GetReportWithDetails(request.info.ReportGuid);

            //null check
            if (report == null) { response.Message = "cant find the report..."; return response; }

            if (report.CreatedBy == _currentUser.UserGuid) { response.Message = "you cant accept your own report!"; return response; }

            //close the report
            report.IsComplete = true;
            await _storyReportRepo.UpdateStatesAsync(report);

            var story = await _storyRepo.GetAsync(report.StoryId);

            if (story == null) { response.Message = "the story deosn't exist anymore!"; return response; }

            var reporter = await _userRepo.GetByGuidAsync(report.CreatedBy);

            if (request.info.IsAccepted)
            {
                //strike the story
                story.IsStriked = true;
                story = await _storyRepo.UpdateStatesAsync(story);

                //delete same repororts
                await _storyReportRepo.RemoveCurrentReports(story.Id);

                //Send a warning to the offending user 
                var targetUser = await _userRepo.GetAsync(report.TargetUserId);

                if (targetUser != null)
                {
                    //add warning
                    Warning warning = new Warning()
                    {
                        UserId = targetUser.Id,
                        Subject = "Violation",
                        Reason = "Violation in the content of the story",
                        Details = "there is no detail"
                    };

                    if (targetUser.Warnings == null)
                        targetUser.Warnings = new List<Warning>();

                    targetUser.Warnings.Add(warning);

                    await _userRepo.UpdateEntityAsync(targetUser);

                    //notification
                    Notification ViolatorNotif = new Notification()
                    {
                        UserId = targetUser.Id,
                        Subject = "A complaint has been filed against you.",
                        Text = $"Dear user, your story ({story.Name}) has been disabled due to a violation of the rules. If this is repeated, your account may be blocked."
                    };

                    if (!string.IsNullOrEmpty(request.info.SpicialMessage))
                        ViolatorNotif.Text += $"\n \n admin message: {request.info.SpicialMessage}";

                    await _notifRepo.AddAsync(ViolatorNotif);

                }

                //reporter notification
                if (reporter != null)
                {
                    Notification reporterNotif = new Notification()
                    {
                        UserId = await _userRepo.GetIdByGuid(report.CreatedBy),
                        Subject = "Your conclusion",
                        Text = $"Dear user, your report has been approved by the admin and the story ({story.Name}) has been unavailable until further notice."
                    };

                    await _notifRepo.AddAsync(reporterNotif);
                }
            }
            else  //report is NOT accepted
            {
                if (reporter != null)
                {
                    Notification notif = new Notification()
                    {
                        UserId = reporter.Id,
                        Subject = "Your conclusion",
                        Text = $"Dear user, your report about the story ({story.Name}) Rejected by admin"
                    };

                    await _notifRepo.AddAsync(notif);
                }
            }

            response.Message = "Your vote was successfully registered.";
            response.Success = true;

            return response;
        }
    }
}


/*
Note:
Before doing anything, the report is closed. 
Because in any case, this report has been read and judged by the admin.
 */
