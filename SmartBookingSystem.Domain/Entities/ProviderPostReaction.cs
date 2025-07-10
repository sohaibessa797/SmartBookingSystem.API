using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class ProviderPostReaction : BaseEntity
    {
        public Guid ProviderPostId { get; set; }
        public ProviderPost? ProviderPost { get; set; }

        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string Emoji { get; set; } = string.Empty;
    }
}
