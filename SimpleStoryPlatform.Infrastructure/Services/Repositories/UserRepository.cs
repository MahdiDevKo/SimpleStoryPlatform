using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using SimpleStoryPlatform.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.Services.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly StoryPlatformDbContext _context;
        public UserRepository(StoryPlatformDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<StoryReleaseRequest>?> GetStoryReleaseRequests(Guid userGuid, bool IsComplete = false)
        {
            var user = await _context.Users
                .Include(u => u.StoryReleaseRequests)
                    .ThenInclude(sr => sr.Object)
                        .ThenInclude(r => r.Object)
                .Include(u => u.StoryReleaseRequests)
                    .ThenInclude(sr => sr.Object)
                        .ThenInclude(sr => sr.TargetUser)
                .FirstOrDefaultAsync(u => u.PublicId == userGuid);

            var result = user.StoryReleaseRequests.Where(sr => sr.IsComplete == IsComplete).ToList();

            return result;
        }

        public async Task<User?> GetUserWithAllDetails(Guid userGuid)
        {
            var user = await _context.Users
                .Include(u => u.Warnings)
                .Include(u => u.Inbox)
                .FirstOrDefaultAsync(u => u.PublicId == userGuid);
            return user;
        }

        public async Task<User?> GetUserWithWarnings(Guid userGuid)
        {
            var user = await _context.Users
                .Include(u => u.Warnings)
                .FirstOrDefaultAsync(u => u.PublicId == userGuid);

            return user;
        }
    }
}
