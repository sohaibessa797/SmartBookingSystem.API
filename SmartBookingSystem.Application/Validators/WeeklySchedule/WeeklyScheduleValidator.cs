using FluentValidation;
using SmartBookingSystem.Application.DTOs.WeeklySchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.WeeklySchedule
{
    public class WeeklyScheduleValidator : AbstractValidator<WeeklyScheduleRequest>
    {
        public WeeklyScheduleValidator()
        {
            RuleFor(x => x.DayOfWeek)
                 .IsInEnum()
                 .WithMessage("Invalid day of the week.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be earlier than end time.");

            RuleFor(x => x)
                .Must(x => (x.EndTime - x.StartTime).TotalMinutes >= 30)
                .WithMessage("The duration must be at least 30 minutes.");
        }
    }
}
