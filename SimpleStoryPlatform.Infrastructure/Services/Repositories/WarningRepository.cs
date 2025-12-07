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
    public class WarningRepository : GenericRepository<Warning>, IWarningRepository
    {
        public WarningRepository(StoryPlatformDbContext context) : base(context)
        {
        }
    }
}
