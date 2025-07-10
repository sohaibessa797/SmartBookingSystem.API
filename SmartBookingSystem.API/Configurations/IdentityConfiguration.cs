using Microsoft.AspNetCore.Identity;
using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Infrastructure.Data;

namespace SmartBookingSystem.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            return services;
        }
    }
}
