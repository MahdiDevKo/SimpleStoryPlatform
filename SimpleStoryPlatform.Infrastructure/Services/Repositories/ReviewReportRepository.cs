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

        public async Task DeleteSameReportsAsync(int reviewId)
        {
            var sameReports = await _context.ReviewReports
                .Where(rr => rr.ReviewId == reviewId)
                .ToListAsync();

            _context.ReviewReports.RemoveRange(sameReports);

            await _context.SaveChangesAsync();
        }

    }
}
