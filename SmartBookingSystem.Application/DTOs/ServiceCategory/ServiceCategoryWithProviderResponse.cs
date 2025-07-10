using SmartBookingSystem.Application.DTOs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.ServiceCategory
{
    public class ServiceCategoryWithProviderResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProviderListItemResponse> Providers { get; set; } = new List<ProviderListItemResponse>();
    }
}
