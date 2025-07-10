using SmartBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Interfaces
{
    public interface IProviderPostRepository : IGenericRepository<ProviderPost>
    {
        Task UpdateAsync(ProviderPost providerPost);
    }
}
