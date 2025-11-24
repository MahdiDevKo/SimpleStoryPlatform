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
    public class ReviewReportRepository : GenericRepository<StoryReviewReport>, IReviewReportRepository
    {
        private readonly StoryPlatformDbContext _context;
        public ReviewReportRepository(StoryPlatformDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StoryReviewReport>> GetAllWithDetail()
        {
            var reports = await _context.ReviewReports
                .Include(r => r.Object)
                    .ThenInclude(s => s.TargetStory)
                .Include(r => r.Object)
                    .ThenInclude(s => s.Reviewer)
                .Include(r => r.TargetUser)
                .Where(r => r.IsComplete == false)
                .ToListAsync();

            return reports;
        }

        public Task<StoryReviewReport?> GetReportWithDetails(Guid reportGuid)
        {
            var report = _context.ReviewReports
                .Include(r => r.Object)  //StoryReview
                .Include(r => r.TargetUser)
                .FirstOrDefaultAsync(r => r.PublicId == reportGuid);

            return report;
        }
    }
}
