using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Service
{
    public class ServiceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public double Price { get; set; } = 0.0;
        public string ProviderName { get; set; }
        public string ServiceCategoryName { get; set; }
    }
}
