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
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(appointment.Id);
            if (existingAppointment != null) 
            { 
                existingAppointment.Status = appointment.Status;
                existingAppointment.Rating = appointment.Rating;
                existingAppointment.AppointmentTime = appointment.AppointmentTime;
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {appointment.Id} not found.");
            }
            await _context.SaveChangesAsync();
        }
    }
}
