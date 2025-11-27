using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;

namespace SimpleStoryPlatform.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WritersController : ControllerBase
    {
        private readonly ILogger<PublicController> _logger;
        private readonly IMediator _mediator;
        public WritersController(ILogger<PublicController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        private Guid GetPublicId()
            => Guid.Parse(User.FindFirst("PublicId")?.Value);
        [HttpPost("Create-New-Story")]
        public async Task<BaseResponseWithData<Guid?>> CreateStory([FromBody] StoryCreateDto createDto)
        {
            createDto.WriterGuid = GetPublicId();

            var command = new StoryCreateCommand() { createDto = createDto};

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpGet("My-Stories")]
        public async Task<BaseResponseWithData<List<StoryPreviewDto>?>> GetMyStories()
        {
            var request = new GetWritedStoriesRequest() {UserGuid = GetPublicId() };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("My-Story-Details")]
        public async Task<BaseResponseWithData<StoryDetailsDto>> GetStoryDetails([FromBody] Guid storyGuid)
        {
            var request = new GetStoryDetailsRequest() { userGuid = GetPublicId(), storyGuid = storyGuid };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Update-Story")]
        public async Task<BaseResponseWithData<StoryDetailsDto>> UpdateStory([FromBody] StoryDetailsDto storyDto)
        {
            var request = new StoryUpdateCommand() 
            {
                storyDto = storyDto,
                userGuid = GetPublicId(),
                Publish = storyDto.IsPublished
            };

            var response = await _mediator.Send(request);

            return response;
        }
        [HttpPost("Unpublish-Story")]
        public async Task<BaseResponse> UnpunblishStory([FromBody] Guid storyGuid)
        {
            var request = new StoryUnpublishCommand() { StoryGuid = storyGuid, UserGuid = GetPublicId()};

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Release-Request")]
        public async Task<BaseResponse> StoryReleaseRequest([FromBody] StoryReleaseRequestCreateDto releaseRequestDto)
        {
            var request = new StoryReleaseRequestCommand() { releaseRequestDto = releaseRequestDto };

            var response = await _mediator.Send(request);

            return response;
        }
    }
}
