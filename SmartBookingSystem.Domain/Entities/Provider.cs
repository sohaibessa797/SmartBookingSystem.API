using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;

        public int WorkingSince { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? FacebookPage { get; set; }
        public string? InstagramHandle { get; set; }

        public bool IsApproved { get; set; } = true;
        public double AverageRating { get; set; } = 0.0;

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public Guid? ServiceCategoryId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<WeeklySchedule> Schedules { get; set; } = new List<WeeklySchedule>();
        public ICollection<ProviderPost> Posts { get; set; } = new List<ProviderPost>();
    }
}
