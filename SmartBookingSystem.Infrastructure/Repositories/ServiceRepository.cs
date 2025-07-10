using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using SmartBookingSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(Service service)
        {
            var existingService = await _context.Services.FindAsync(service.Id);
            if (existingService == null)
            {
                throw new KeyNotFoundException("Service not found.");
            }
            existingService.Name = service.Name;
            existingService.Description = service.Description;
            existingService.Price = service.Price;
            existingService.ServiceCategoryId = service.ServiceCategoryId;
            await _context.SaveChangesAsync();
        }
    }
}
