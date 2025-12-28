using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;

namespace SimpleStoryPlatform.API.Controllers
{
    [Authorize(Roles = "owner,admin")]
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : Controller
    {
        //owner features need to be complete
        [HttpPost("Get-Admins")]
        public async Task<PageResponse<UserPreviewDto>> GetWarnings([FromBody] BaseRequest req)
        {
            return new PageResponse<UserPreviewDto>();
        }
    }
}
