using SimpleStoryPlatform.Domain.Entites;

namespace SimpleStoryPlatform.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
