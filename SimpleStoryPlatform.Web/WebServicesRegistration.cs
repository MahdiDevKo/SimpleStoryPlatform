namespace SimpleStoryPlatform.Web
{
    public static class WebServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableWebCore", builder =>
                {
                    builder
                    .WithOrigins("https://localhost:5055")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .Build();
                });
            });

            return services;
        }
    }
}
