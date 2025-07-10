using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class ProviderPost : BaseEntity
    {
        public Guid ProviderId { get; set; }
        public Provider? Provider { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public ICollection<ProviderPostImage> Images { get; set; } = new List<ProviderPostImage>();
        public ICollection<ProviderPostReaction> Reactions { get; set; } = new List<ProviderPostReaction>();
    }
}
