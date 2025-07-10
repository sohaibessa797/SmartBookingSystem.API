using Microsoft.EntityFrameworkCore;
using SmartBookingSystem.Infrastructure.Data;

namespace SmartBookingSystem.API.Configurations
{
    public static class ConnectionWithDbConfiguration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

    }
}
