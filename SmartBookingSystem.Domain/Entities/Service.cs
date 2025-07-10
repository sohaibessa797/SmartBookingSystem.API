using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; } = 0.0;
        public Guid ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }
    }
}
