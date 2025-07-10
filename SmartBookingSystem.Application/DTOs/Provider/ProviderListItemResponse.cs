using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Provider
{
    public class ProviderListItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string ShortDescription { get; set; }
        public string ServiceCategoryName { get; set; }
        public double AverageRating { get; set; }
    }
}
