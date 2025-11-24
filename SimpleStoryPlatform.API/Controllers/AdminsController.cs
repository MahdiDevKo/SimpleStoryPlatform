using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Admins.Handlers.Commands;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Commands;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Queries;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;

namespace SimpleStoryPlatform.API.Controllers
{
    [Authorize(Roles = "owner,admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("Available-Reports")]
        public async Task<BaseResponseWithData<AllReportsDto>> AvailableReports()
        {
            var request = new AdminsGetAvailableReportsRequest();

            var response = await _mediator.Send(request);

            return response;
        }

        #region Complete a report

        [HttpPost("Complete-Report-Story")]
        public async Task<BaseResponse> CompleteStoryReport([FromBody] ReportCompleteDto<StoryReportDto?> dto)
        {
            var request = new AdminsCompleteReportCommand<StoryReportDto>(){reportInfo = dto};

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Complete-Report-Review")]
        public async Task<BaseResponse> CompleteReviewReport([FromBody] ReportCompleteDto<ReviewReportDto?> dto)
        {
            var request = new AdminsCompleteReportCommand<ReviewReportDto>() { reportInfo = dto };

            var response = await _mediator.Send(request);
            
            return response;
        }

        [HttpPost("Complete-Report-ReleaseRequest")]
        public async Task<BaseResponse> CompleteReleaseRequest([FromBody] ReportCompleteDto<StoryReleaseRequestDetailsDto?> dto)
        {
            var request = new AdminsCompleteReportCommand<StoryReleaseRequestDetailsDto>() { reportInfo = dto };

            var response = await _mediator.Send(request);

            return response;
        }

        #endregion
    }
}
