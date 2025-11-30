using MediatR;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Commands
{
    public class ReleaseRequestCompleteCommandHandler : IRequestHandler<ReleaseRequestCompleteCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IStoryReleaseRepository _releaseRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly INotificationRepository _notifRepo;
        private readonly ICurrentUserToken _currentUser;
        public ReleaseRequestCompleteCommandHandler(
            IStoryReleaseRepository storyReleaseRepository,
            IStoryRepository storyRepo,
            INotificationRepository notifRepo,
            IUserRepository userRepository,
            ICurrentUserToken currentUserToken
            )
        {
            _releaseRepo = storyReleaseRepository;
            _storyRepo = storyRepo;
            _notifRepo = notifRepo;
            _userRepo = userRepository;
            _currentUser = currentUserToken;
        }
        public async Task<BaseResponse> Handle(ReleaseRequestCompleteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var releaseRequest = await _releaseRepo.GetByGuidAsync(request.info.ReportGuid);

            //null check
            if (releaseRequest == null) { response.Message = "this report deosn't exsits anymore..."; return response; }

            if (releaseRequest.CreatedBy == _currentUser.UserGuid) { response.Message = "you cant accept your own report!"; return response; }

            //complete the request
            releaseRequest.IsComplete = true;
            await _releaseRepo.UpdateStatesAsync(releaseRequest);

            var writer = await _userRepo.GetByGuidAsync(releaseRequest.CreatedBy);

            var story = await _storyRepo.GetAsync(releaseRequest.StoryId);

            //null check
            if (story == null || story.IsDeleted) { response.Message = "the story deosn't exist anymore..."; return response; }
            
            if (request.info.IsAccepted)
            {
                //release the story
                story.IsStriked = false;
                await _storyRepo.UpdateStatesAsync(story);

                //send notif
                if (writer != null)
                {
                    Notification notif = new Notification()
                    {
                        UserId = writer.Id,
                        Subject = "Release request result",
                        Text = $"Dear user, your request has been accepted by the admin and the story ({story.Name}) is now free."
                    };

                    await _notifRepo.AddAsync(notif);
                }

            }
            else
            {
                //send notif
                if (writer != null)
                {
                    Notification notif = new Notification()
                    {
                        UserId = writer.Id,
                        Subject = "Release request result",
                        Text = $"Dear user, Your request to release story ({story.Name}) has been denied."
                    };

                    if (!string.IsNullOrEmpty(request.info.SpicialMessage))
                        notif.Text += $"\n\nAdmin message: {request.info.SpicialMessage}";

                    await _notifRepo.AddAsync(notif);
                }
            }

            response.Message = "Your vote was successfully registered.";
            response.Success = true;
            return response;
        }
    }
}
