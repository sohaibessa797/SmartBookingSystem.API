using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class ProviderPostImage : BaseEntity
    {
        public Guid ProviderPostId { get; set; }
        public ProviderPost? ProviderPost { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
