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
    public class StoryReleaseRepository : GenericRepository<StoryReleaseRequest>, IStoryReleaseRepository
    {
        private readonly StoryPlatformDbContext _context;
        public StoryReleaseRepository(StoryPlatformDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
