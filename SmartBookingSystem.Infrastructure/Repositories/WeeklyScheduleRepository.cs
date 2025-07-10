using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using SmartBookingSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Repositories
{
    public class WeeklyScheduleRepository : GenericRepository<WeeklySchedule>, IWeeklyScheduleRepository

    {
        public WeeklyScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(WeeklySchedule weeklySchedule)
        {
            var existingSchedule = await _context.WeeklySchedules.FindAsync(weeklySchedule.Id);
            if (existingSchedule != null)
            {
                existingSchedule.DayOfWeek = weeklySchedule.DayOfWeek;
                existingSchedule.StartTime = weeklySchedule.StartTime;
                existingSchedule.EndTime = weeklySchedule.EndTime;
                _context.WeeklySchedules.Update(existingSchedule);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Weekly schedule not found.");
            }
        }
    }
}
