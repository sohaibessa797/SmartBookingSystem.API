using SmartBookingSystem.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class WeeklySchedule : BaseEntity
    {
        public Guid ProviderId { get; set; }
        public Provider? Provider { get; set; }

        public DayOfWeek DayOfWeek { get; set; }   // Saturday, Sunday...
        public TimeSpan StartTime { get; set; }    // 09:00
        public TimeSpan EndTime { get; set; }      // 20:00
    }
}
