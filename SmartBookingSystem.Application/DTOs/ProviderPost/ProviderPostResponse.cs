using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.ProviderPost
{
    public class ProviderPostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ProviderName { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<string> ImageUrls { get; set; } = new();
        public List<ProviderPostReactionResponse> Reactions { get; set; } = new();
    }
}
