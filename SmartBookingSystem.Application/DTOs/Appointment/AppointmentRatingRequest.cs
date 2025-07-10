using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Appointment
{
    public class AppointmentRatingRequest
    {
        public int Rating { get; set; } 
        public string? Feedback { get; set; }
    }
}
