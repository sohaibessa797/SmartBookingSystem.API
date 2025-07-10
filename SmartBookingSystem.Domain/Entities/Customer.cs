using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string profilePicture { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }


        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<ProviderPostReaction> Reactions { get; set; } = new List<ProviderPostReaction>();

    }
}
