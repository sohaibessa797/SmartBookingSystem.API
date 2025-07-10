using FluentValidation;
using SmartBookingSystem.Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.Appointment
{
    public class AppointmentRatingValidator : AbstractValidator<AppointmentRatingRequest>
    {
        public AppointmentRatingValidator() 
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Feedback)
                .MaximumLength(500)
                .WithMessage("Feedback must not exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Feedback))
                .Must(desc => !desc.Contains("<") && !desc.Contains(">"))
                .WithMessage("Description should not contain HTML tags.");
        }
    }
}
