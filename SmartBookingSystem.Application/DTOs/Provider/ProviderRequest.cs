using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Provider
{
    public class ProviderRequest
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Description { get; set; } 
        public string ProfilePicture { get; set; } 
        public string CoverImageUrl { get; set; } 

        public string PhoneNumber { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } 
        public string City { get; set; } 
        public string Country { get; set; } 
        public Guid? ServiceCategoryId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; } 

        public int WorkingSince { get; set; }
        public string WebsiteUrl { get; set; }
        public string FacebookPage { get; set; }
        public string InstagramHandle { get; set; }

    }
}
