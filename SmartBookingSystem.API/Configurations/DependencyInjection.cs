using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Infrastructure.Services;

namespace SmartBookingSystem.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IWeeklyScheduleService, WeeklyScheduleService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IProviderPostService, ProviderPostService>();
            return services; 
        }
    }
}
