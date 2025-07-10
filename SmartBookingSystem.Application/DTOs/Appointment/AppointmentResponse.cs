using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.Appointment
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string ProviderName { get; set; }
        public string ServiceName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
    }
}
