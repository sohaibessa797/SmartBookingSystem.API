using SmartBookingSystem.Domain.Base;
using SmartBookingSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public Guid ProviderId { get; set; }
        public Provider? Provider { get; set; }

        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }

        public DateTime AppointmentTime { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public int? Rating { get; set; } // Optional after Done
        public string? Feedback { get; set; }

    }
}
