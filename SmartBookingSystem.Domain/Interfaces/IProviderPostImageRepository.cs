using SmartBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Interfaces
{
    public interface IProviderPostImageRepository : IGenericRepository<ProviderPostImage>
    {
        Task DeleteRangeAsync(List<ProviderPostImage> entities);
    }
}
