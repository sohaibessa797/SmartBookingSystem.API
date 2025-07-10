using SmartBookingSystem.Domain.Interfaces;
using SmartBookingSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProviderRepository Providers { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IServiceCategoryRepository ServiceCategories { get; private set; }
        public IServiceRepository Services { get; private set; }
        public IWeeklyScheduleRepository WeeklySchedules { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IProviderPostRepository ProviderPosts { get; private set; }
        public IProviderPostImageRepository ProviderPostImages { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Providers = new ProviderRepository(_context);
            Customers = new CustomerRepository(_context);
            ServiceCategories = new ServiceCategoryRepository(_context);
            Services = new ServiceRepository(_context);
            WeeklySchedules = new WeeklyScheduleRepository(_context);
            Appointments = new AppointmentRepository(_context);
            ProviderPosts = new ProviderPostRepository(_context);
            ProviderPostImages = new ProviderPostImageRepository(_context);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
