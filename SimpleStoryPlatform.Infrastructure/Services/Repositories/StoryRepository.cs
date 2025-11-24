using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.Services.Repositories
{
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        private readonly StoryPlatformDbContext _context;
        public StoryRepository(StoryPlatformDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string?> AddStoryReview(StoryReview review)
        {
            try
            {
                _context.StoryReviews.Add(review);
                await _context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<Story> GetStoryDetails(Guid storyGuid)
        {
            var story = await _context.Stories
                .Include(s => s.Data)
                .Include(s => s.Writer)
                .Include(s => s.Reviews)
                .Include(s => s.PlayList)
                .Include(s => s.Reports)
                .Include(s => s.ReleaseRequests)
                .FirstOrDefaultAsync(s => s.PublicId == storyGuid);

            return story;
        }

        public async Task<List<Story>> GetWritedStories(Guid userGuid)
        {
            var stories = await _context.Stories
                .Include(s => s.Reviews)
                .Include(s => s.Data)
                .Include(s => s.Writer)
                .Where(s => s.CreatedBy == userGuid).ToListAsync();

            return stories;
        }

        public async Task<List<Story>?> SearchStories(string? searchValue, bool isAdmin = false)
        {
            IQueryable<Story> query = _context.Set<Story>()
                .Include(s => s.Writer)
                .Include(s => s.Data)
                .Include(s => s.Reviews);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(s => s.Name.Contains(searchValue));

            if (!isAdmin)
                query = query.Where(s => !s.IsStriked);

            var result = await query.Where(s => s.IsPublished).ToListAsync();

            return result;
        }
    }
}
