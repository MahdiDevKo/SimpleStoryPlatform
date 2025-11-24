using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Identity.Services
{
    public class CurrentUserToken : ICurrentUserToken
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserToken(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserGuid
        {
            get
            {
                var guid =  _httpContextAccessor.HttpContext?.User?
                    .FindFirst("PublicId")?.Value;

                return string.IsNullOrEmpty(guid) ? null : Guid.Parse(guid);
            }
        }
        public string? UserRole
        {
            get
            {
                string? role = _httpContextAccessor.HttpContext?.User?
                    .FindFirst("Role")?.Value;

                return string.IsNullOrEmpty(role) ? "default" : role;
            }
        }
    }
}
