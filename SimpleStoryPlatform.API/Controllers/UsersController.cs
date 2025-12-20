using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SimpleStoryPlatform.API.Controllers;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Identity;
using System.Security.Claims;

namespace SimpleStoryPlatform.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        private Guid GetPublicId()
        => Guid.Parse(User.FindFirst("PublicId")?.Value);

        private string GetRole()
        => User.FindFirst("Role")?.Value.ToString();

        [HttpGet("Profile")]
        public async Task<BaseResponseWithData<UserDetailsDto>> GetUserDetails()
        {
            var request = new UserDetailsRequest();

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Add-Review")]
        public async Task<BaseResponse> AddReview([FromBody] StoryReviewCreateDto reviewCreateDto)
        {
            var request = new UserCreateStoryReviewCommand() { createReviewDto = reviewCreateDto };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Report-Review")]
        public async Task<BaseResponse> ReportReview([FromBody] UserReportDto reportDto)
        {
            var request = new UserReportReviewCommand() { reportDto = reportDto };

            var response = await _mediator.Send(request);

            return response;
        }
        [HttpPost("Report-Story")]
        public async Task<BaseResponse> ReportStory([FromBody] UserReportDto reportDto)
        {
            var request = new UserReportStoryCommand() { reportDto = reportDto };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Add-Story-To-Library")]
        public async Task<BaseResponse> AddStory([FromBody] Guid storyGuid)
        {
            var request = new UserAddStoryToLibraryCommand() { storyGuid = storyGuid };

            var response = await _mediator.Send(request);

            return response;
        }


        [HttpPost("Get-Library")]
        public async Task<PageResponse<StoryPreviewDto>> GetLibrary([FromBody] BaseRequest? req)
        {
            var request = new UserGetLibraryRequest();

            if (req != null)
                request.reqProp = req;

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Get-Notifications")]
        public async Task<PageResponse<NotificationDto>> GetNotifications([FromBody] BaseRequest req)
        {
            var request = new UserGetNorificationsRequest() { req = req };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Get-Warnings")]
        public async Task<PageResponse<WarningDto>> GetWarnings([FromBody] BaseRequest req)
        {
            var request = new UserGetWarningsRequest() { req = req };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpGet("Read-Notification")]
        public async Task<BaseResponse> ReadNotification(Guid notifGuid)
        {
            var request = new UserReadNotificationRequest() { notifGuid = notifGuid };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpGet("Read-Warning")]
        public async Task<BaseResponse> ReadWarning(Guid warningGuid)
        {
            var request = new UserReadWarningRequest() { warningGuid = warningGuid };

            var response = await _mediator.Send(request);

            return response;
        }
    }
}