using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.DbSettings
{
    public class StoryPlatformDbContextFactory : IDesignTimeDbContextFactory<StoryPlatformDbContext>
    {
        public StoryPlatformDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\SimpleStoryPlatform.API"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<StoryPlatformDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            var fakeUserToken = new FakeCurrentUserToken();

            return new StoryPlatformDbContext(builder.Options, fakeUserToken);
        }
        private class FakeCurrentUserToken : ICurrentUserToken
        {
            public Guid? UserGuid => null; // یا Guid.Empty اگه خواستی
        }
    }

}
