using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Users.Requests.Commands;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Controllers;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Identity;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;
        public PublicController(ITokenService tokenService, IMediator mediator)
        {
            _tokenService = tokenService;
            _mediator = mediator;
        }
        private string? GetRole()
        => User.FindFirst("Role")?.Value.ToString();

        [HttpGet("LastStories")]
        public async Task<PageResponse<StoryPreviewDto>> GetLastStories()
        {
            var request = new UserSearchStoryRequest()
            {
                info = new SearchRequest<StorySearchOptionsDto>()
                {
                    PageNumber = 1,
                    Options = new StorySearchOptionsDto()
                }
            };

            var response = await _mediator.Send(request);

            response.Items = response.Items
                .OrderByDescending(s => s.CreatedAt)
                .ToList();

            return response;
        }

        [HttpPost("Read-Story")]
        public async Task<BaseResponseWithData<StoryDetailsDto?>> ReadStory([FromBody] Guid storyGuid)
        {
            var request = new UserReadStoryRequest() { storyGuid = storyGuid };

            //check for token and role
            if (!string.IsNullOrEmpty(GetRole()))
                request.IsAdmin = (GetRole() == "admin" || GetRole() == "owner");

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<BaseResponseWithData<string>>> login([FromBody] UserLoginDto dto)
        {
            var result = new BaseResponseWithData<string>();

            var request = new UserLoginRequest() { loginDto = dto };

            var response = await _mediator.Send(request);

            result.Message = response.Message;

            if (response.Success)
            {
                result.Success = true;
                result.data = _tokenService.GenerateToken(response.data);
            }

            return result;
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<BaseResponseWithData<string>>> signup([FromBody] UserCreateDto dto)
        {
            var result = new BaseResponseWithData<string>();

            var request = new UserCreateCommandRequest() { createDto = dto };

            var response = await _mediator.Send(request);

            result.Message = response.Message;

            if (response.Success)
            {
                result.Success = true;
                result.data = _tokenService.GenerateToken(response.data);
            }

            return result;
        }

        [HttpPost("Search-Result")]
        public async Task<ActionResult<PageResponse<StoryPreviewDto>>> SearchStory([FromBody] SearchRequest<StorySearchOptionsDto>? searchOtions)
        {
            var request = new UserSearchStoryRequest();

            if (searchOtions != null)
                request.info = searchOtions;

            var response = await _mediator.Send(request);

            return response;
        }

    }
}
