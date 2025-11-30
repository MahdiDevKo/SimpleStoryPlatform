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
using SimpleStoryPlatform.Application.Requests;
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

        #region Get a type of report
        [HttpPost("Available-Story-Reports")]
        public async Task<PageResponse<StoryReportDto>> AvailableStoryReports([FromBody] BaseRequest? req)
        {
            var request = new GetStoryReportsRequest();

            if (req != null)
                request.pageReq = req;

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Available-Review-Reports")]
        public async Task<PageResponse<ReviewReportDto>> AvailableReviewReports([FromBody] BaseRequest? req)
        {
            var request = new GetReviewReportsRequest();

            if (req != null)
                request.pageReq = req;

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Available-ReleaseRequests")]
        public async Task<PageResponse<StoryReleaseRequestDetailsDto>> AvailableReleaseRequests([FromBody] BaseRequest? req)
        {
            var request = new GetReleaseRequestsRequest();

            if (req != null)
                request.pageReq = req;

            var response = await _mediator.Send(request);

            return response;
        }

        #endregion
        #region Complete a report

        [HttpPost("Complete-Report-Story")]
        public async Task<BaseResponse> CompleteStoryReport([FromBody] ReportCompleteDto dto)
        {
            var request = new StoryReportCompleteCommand(){info = dto};

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Complete-Report-Review")]
        public async Task<BaseResponse> CompleteReviewReport([FromBody] ReportCompleteDto dto)
        {
            var request = new StoryReviewReportCompleteCommand() { info = dto };

            var response = await _mediator.Send(request);
            
            return response;
        }

        [HttpPost("Complete-Report-ReleaseRequest")]
        public async Task<BaseResponse> CompleteReleaseRequest([FromBody] ReportCompleteDto dto)
        {
            var request = new ReleaseRequestCompleteCommand() { info = dto };

            var response = await _mediator.Send(request);

            return response;
        }

        #endregion
    }
}
