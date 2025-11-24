using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Infrastructure.DbSettings;
using SimpleStoryPlatform.Infrastructure.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoryPlatformDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IStoryReviewRepository, StoryReviewRepository>();

            services.AddScoped<IStoryReportRepository, StoryReportRepository>();
            services.AddScoped<IReviewReportRepository, ReviewReportRepository>();
            services.AddScoped<IStoryReleaseRepository, StoryReleaseRepository>();

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddAuthorization();

            return services;
        }
    }
}
