using AutoMapper;
using SmartBookingSystem.Application.DTOs.Appointment;
using SmartBookingSystem.Application.DTOs.Service;
using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Enum;
using SmartBookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppointmentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;        
        }

        public async Task<List<AppointmentResponse>> GetProviderAppointmentsAsync(Guid userId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");

            var appointments = await _unitOfWork.Appointments.GetAllAsync(
                a => a.ProviderId == provider.Id,
                x => x.Customer, x => x.Provider, x => x.Service);

            return _mapper.Map<List<AppointmentResponse>>(appointments);
        }

        public async Task<List<AppointmentResponse>> GetCustomerAppointmentsAsync(Guid userId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.ApplicationUserId == userId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            var appointments = await _unitOfWork.Appointments.GetAllAsync(
                a => a.CustomerId == customer.Id,
                x => x.Customer, x => x.Provider, x => x.Service);

            return _mapper.Map<List<AppointmentResponse>>(appointments);
        }



        public async Task<AppointmentResponse> CreateAppointmentAsync(Guid userId, AppointmentRequest request)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.ApplicationUserId == userId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.Id == request.ProviderId);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");

            var service = await _unitOfWork.Services.GetByIdAsync(s => s.Id == request.ServiceId && s.ProviderId == provider.Id);
            if (service == null)
                throw new InvalidOperationException("Service not found or doesn't belong to this provider.");

            // Check the weekly schedule
            var schedules = await _unitOfWork.WeeklySchedules.GetAllAsync(s => s.ProviderId == provider.Id);
            var appointmentDay = request.AppointmentTime.DayOfWeek;
            var appointmentTime = request.AppointmentTime.TimeOfDay;

            var validDay = schedules.FirstOrDefault(s =>
                s.DayOfWeek == appointmentDay &&
                s.StartTime <= appointmentTime &&
                s.EndTime >= appointmentTime);

            if (validDay == null)
                throw new InvalidOperationException("This appointment is outside the provider's working hours.");

            // Check that there is no appointment already at the same time
            var overlapping = await _unitOfWork.Appointments.AnyAsync(a =>
            a.ProviderId == provider.Id &&
                a.AppointmentTime == request.AppointmentTime);

            if (overlapping)
                throw new InvalidOperationException("This appointment time is already booked.");

            var appointment = _mapper.Map<Appointment>(request);
            appointment.CustomerId = customer.Id;


            //// create appointment
            //var appointment = new Appointment
            //{
            //    CustomerId = customer.Id,
            //    ProviderId = provider.Id,
            //    ServiceId = service.Id,
            //    AppointmentTime = request.AppointmentTime,
            //    Status = AppointmentStatus.Pending
            //};

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            var appointmentWithDetails = await _unitOfWork.Appointments.GetByIdAsync(
              a => a.Id == appointment.Id,
              x => x.Customer, x => x.Provider, x => x.Service);


            var response = _mapper.Map<AppointmentResponse>(appointmentWithDetails);
            return response;


            //// Response
            //var response = new AppointmentResponse
            //{
            //    Id = appointment.Id,
            //    CustomerName = $"{customer.FirstName} {customer.LastName}",
            //    ProviderName = $"{provider.FirstName} {provider.LastName}",
            //    ServiceName = service.Name,
            //    AppointmentTime = appointment.AppointmentTime,
            //    Status = appointment.Status.ToString()
            //};

        }



        public async Task<string> ConfirmAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(a => a.Id == appointmentId);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            if (appointment.Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only pending appointments can be confirmed.");

            appointment.Status = AppointmentStatus.Confirmed;
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return "Appointment confirmed successfully.";
        }

        public async Task<string> MarkAppointmentAsDoneAsync(Guid appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(a => a.Id == appointmentId);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            if (appointment.Status != AppointmentStatus.Confirmed)
                throw new InvalidOperationException("Only confirmed appointments can be marked as done.");

            if (appointment.AppointmentTime > DateTime.UtcNow)
                throw new InvalidOperationException("You can only mark past appointments as done.");

            appointment.Status = AppointmentStatus.Done;
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return "Appointment marked as done.";
        }

        public async Task<string> CancelAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(a => a.Id == appointmentId);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            appointment.Status = AppointmentStatus.Cancelled;
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return "Appointment canceled successfully.";
        }

        public async Task<string> RateAppointmentAsync(Guid userId, Guid appointmentId, AppointmentRatingRequest request)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.ApplicationUserId == userId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            var appointment = await _unitOfWork.Appointments.GetByIdAsync(a=>a.Id ==  appointmentId);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            if (appointment.CustomerId != customer.Id)
                throw new UnauthorizedAccessException("You are not allowed to rate this appointment.");

            if (appointment.Status != AppointmentStatus.Done)
                throw new InvalidOperationException("Only completed appointments can be rated.");

            if (appointment.Rating.HasValue)
                throw new InvalidOperationException("This appointment has already been rated.");

            appointment.Rating = request.Rating;
            appointment.Feedback = request.Feedback;


            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            return "Appointment rated successfully.";
        }
    }
}
