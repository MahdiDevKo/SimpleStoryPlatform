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
    public class StoryReviewReportCompleteCommandHandler : IRequestHandler<StoryReviewReportCompleteCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IReviewReportRepository _reviewReportRepo;
        private readonly IStoryReviewRepository _storyReviewRepo;
        private readonly INotificationRepository _notifRepo;
        public StoryReviewReportCompleteCommandHandler(
            IUserRepository userRepository,
            IReviewReportRepository reviewReportRepository,
            IStoryReviewRepository storyReviewRepo,
            INotificationRepository notifRepo)
        {
            _userRepo = userRepository;
            _reviewReportRepo = reviewReportRepository;
            _storyReviewRepo = storyReviewRepo;
            _notifRepo = notifRepo;
        }
        public async Task<BaseResponse> Handle(StoryReviewReportCompleteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var report = await _reviewReportRepo.GetReportWithDetails(request.info.ReportGuid);

            //null check
            if (report == null) { response.Message = "cant find the report..."; return response; }

            //close the report
            report.IsComplete = true;
            await _reviewReportRepo.UpdateStatesAsync(report);

            var review = await _storyReviewRepo.GetAsync(report.ReviewId);

            if (review == null || review.IsDeleted) { response.Message = "the Review is deleted already...!"; return response; }

            var reporter = await _userRepo.GetByGuidAsync(report.CreatedBy);

            if (request.info.IsAccepted)
            {
                //delete the story
                review.IsDeleted = true;
                await _storyReviewRepo.UpdateStatesAsync(review);

                //delete same repororts
                await _reviewReportRepo.DeleteSameReportsAsync(review.Id);

                //Send a warning to the offending user 
                var targetUser = await _userRepo.GetAsync(report.TargetUserId);

                if (targetUser != null)
                {
                    //add warning
                    Warning warning = new Warning()
                    {
                        UserId = targetUser.Id,
                        Subject = "Violation",
                        Reason = "Violation in the content of comment",
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
                        Text = $"Dear user, your comment ({review.Data}) has been deleted due to a violation of the rules. If this is repeated, your account may be blocked."
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
                        Text = $"Dear user, your report has been approved by the admin and the comment ({review.Data}) has been deleted."
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
                        Text = $"Dear user, your report about the comment ({review.Data}) Rejected by admin"
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

