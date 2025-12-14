using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using SimpleStoryPlatform.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<bool> AddToLibraryAsycn(Guid? userGuid,Story storyGuid)
        {
            var user = await _context.Users
                .Include(u => u.Library)
                .FirstOrDefaultAsync(u => u.PublicId == userGuid);

            if (user == null) return false;

            user.Library.Add(storyGuid);

            await SaveAsync(user);

            return true;
        }


        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<PageResponse<Story>> GetLibraryPage(Guid? userGuid, BaseRequest req)
        {
            var response = new PageResponse<Story>()
            {
                PageSize = req.PageSize,
                CurrentPage = req.PageNumber
            };

            var query = _context.Users
                .Include(u => u.Library)
                    .ThenInclude(s => s.Writer);

            var user = await query.FirstOrDefaultAsync(u => u.PublicId == userGuid);
                
            if(user != null)
            {
                response.TotalItems = user.Library.Count;
                response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)response.PageSize);
            }
            var items = user.Library
                .Skip((req.PageNumber - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();

            response.Items = items;

            return response;
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

        public async Task<bool> IsInLibrary(Guid? userGuid, Guid storyGuid)
        {
            var user = await _context.Users
                .Include(u => u.Library)
                .FirstOrDefaultAsync(u => u.PublicId ==  userGuid);

            if(user == null) return false;  

            return user.Library.Any(s => s.PublicId == storyGuid);
        }
    }
}
