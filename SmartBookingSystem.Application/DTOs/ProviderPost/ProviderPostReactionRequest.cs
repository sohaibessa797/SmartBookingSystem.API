using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.ProviderPost
{
    public class ProviderPostReactionRequest
    {
        public Guid PostId { get; set; }
        public Guid CustomerId { get; set; }
        public string Emoji { get; set; }
    }
}
