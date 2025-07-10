using SmartBookingSystem.Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponse> CreateAppointmentAsync(Guid userId, AppointmentRequest request);
        Task<List<AppointmentResponse>> GetCustomerAppointmentsAsync(Guid userId);
        Task<List<AppointmentResponse>> GetProviderAppointmentsAsync(Guid userId);
        Task<string> CancelAppointmentAsync(Guid appointmentId);
        Task<string> ConfirmAppointmentAsync(Guid appointmentId);
        Task<string> MarkAppointmentAsDoneAsync(Guid appointmentId);

        Task<string> RateAppointmentAsync(Guid userId, Guid appointmentId, AppointmentRatingRequest request);

    }
}
