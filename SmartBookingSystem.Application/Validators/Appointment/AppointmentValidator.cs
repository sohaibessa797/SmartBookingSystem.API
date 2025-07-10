using FluentValidation;
using FluentValidation.Validators;
using SmartBookingSystem.Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.Appointment
{
    public class AppointmentValidator : AbstractValidator<AppointmentRequest>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.ServiceId)
               .NotEmpty().WithMessage("Service ID is required.");

            RuleFor(x => x.ProviderId)
                .NotEmpty().WithMessage("Provider ID is required.");

            RuleFor(x => x.AppointmentTime)
                .NotEmpty().WithMessage("Appointment date is required.")
                .Must(BeAFutureDate).WithMessage("Appointment date must be in the future.");
        }

        private bool BeAFutureDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }
    }
}

