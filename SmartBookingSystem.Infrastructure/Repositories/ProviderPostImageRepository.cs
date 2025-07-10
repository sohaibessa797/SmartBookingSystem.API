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
    public class ProviderPostImageRepository : GenericRepository<ProviderPostImage>, IProviderPostImageRepository
    {
        public ProviderPostImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task DeleteRangeAsync(List<ProviderPostImage> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

    }
}
