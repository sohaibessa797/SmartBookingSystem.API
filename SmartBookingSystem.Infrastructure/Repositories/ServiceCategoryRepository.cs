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
    public class ServiceCategoryRepository : GenericRepository<ServiceCategory>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(ServiceCategory serviceCategory)
        {
            var existingServiceCategory = await _context.ServiceCategories.FindAsync(serviceCategory.Id);
            if (existingServiceCategory != null)
            {
                existingServiceCategory.Name = serviceCategory.Name;
                existingServiceCategory.Description = serviceCategory.Description;
            }
            else
            {
                throw new KeyNotFoundException($"Service Category with ID {serviceCategory.Id} not found.");
            }
            await _context.SaveChangesAsync();
        }
    }
}
