using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites.Report;
using SimpleStoryPlatform.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.Services.Repositories
{
    public class StoryReportRepository : GenericRepository<StoryReport>, IStoryReportRepository
    {
        private readonly StoryPlatformDbContext _context;
        public StoryReportRepository(StoryPlatformDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveCurrentReports(int storyId)
        {
            var story = await _context.Stories
                .Include(s => s.Reports)
                .FirstOrDefaultAsync(s => s.Id == storyId);

            var targetReports = story.Reports.Where(r => r.IsComplete == false).ToList();

            foreach (var report in targetReports)
                story.Reports.Remove(report);

            await _context.SaveChangesAsync();
        }
    }
}
