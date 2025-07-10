using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.DTOs.WeeklySchedule
{
    public class WeeklyScheduleResponse
    {
        public Guid Id { get; set; }
        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ProviderName { get; set; }
    }
}
