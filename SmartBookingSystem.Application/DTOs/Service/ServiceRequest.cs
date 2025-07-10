using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Service
{
    public class ServiceRequest
    {
        public string Name { get; set; } 
        public string Description { get; set; } 
        public double Price { get; set; } 
        public Guid ServiceCategoryId { get; set; }
    }
}
