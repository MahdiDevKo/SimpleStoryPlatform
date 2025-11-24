using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsername(string username);
        Task<User?> GetUserWithWarnings(Guid userGuid);
        Task<List<StoryReleaseRequest>?> GetStoryReleaseRequests(Guid userGuid, bool IsComplete = false);
        Task<User?> GetUserWithAllDetails(Guid userGuid);

    }
}
