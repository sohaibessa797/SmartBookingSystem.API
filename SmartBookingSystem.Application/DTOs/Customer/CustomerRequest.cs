using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Customer
{
    public class CustomerRequest
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string profilePicture { get; set; } 
        public string PhoneNumber { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } 
        public string city { get; set; } 
        public string country { get; set; } 
    }
}
