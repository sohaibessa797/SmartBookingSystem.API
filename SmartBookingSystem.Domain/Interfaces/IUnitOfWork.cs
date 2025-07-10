using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProviderRepository Providers { get; }
        ICustomerRepository Customers { get; }
        IServiceCategoryRepository ServiceCategories { get; }
        IServiceRepository Services { get; }
        IWeeklyScheduleRepository WeeklySchedules { get; }
        IAppointmentRepository Appointments { get; }
        IProviderPostRepository ProviderPosts { get; }
        IProviderPostImageRepository ProviderPostImages { get; }
        Task<int> SaveChangesAsync();
        void Dispose(); 
    }
}
