using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Appointment
{
    public class AppointmentRequest
    {
        public Guid ServiceId { get; set; }
        public Guid ProviderId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}
