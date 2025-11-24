using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SimpleStoryPlatform.API.Controllers;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
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
            var request = new UserDetailsRequest() { userGuid = GetPublicId() };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Add-Review")]
        public async Task<BaseResponse> AddReview([FromBody] StoryReviewCreateDto reviewCreateDto)
        {
            reviewCreateDto.UserGuid = GetPublicId();

            var request = new UserCreateStoryReviewCommand() { createReviewDto = reviewCreateDto };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Report-Review")]
        public async Task<BaseResponse> ReportReview([FromBody] UserReportDto reportDto)
        {
            reportDto.UserGuid = GetPublicId();

            var request = new UserReportReviewCommand() {reportDto = reportDto};

            var response = await _mediator.Send(request);

            return response;
        }
        [HttpPost("Report-Story")]
        public async Task<BaseResponse> ReportStory([FromBody] UserReportDto reportDto)
        {
            reportDto.UserGuid = GetPublicId();

            var request = new UserReportStoryCommand() { reportDto = reportDto};

            var response = await _mediator.Send(request);

            return response;
        }
    }
}
