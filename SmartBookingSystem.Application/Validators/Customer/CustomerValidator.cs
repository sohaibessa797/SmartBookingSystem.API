using FluentValidation;
using SmartBookingSystem.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.Customer
{
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be a valid international format.");
            RuleFor(x => x.profilePicture)
                .Matches(@"^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w.-]*)*/?$")
                .When(x => !string.IsNullOrEmpty(x.profilePicture))
                .WithMessage("Invalid profile picture URL.");
            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.")
                .Must(date => date <= DateTime.Now.AddYears(-10))
                .WithMessage("Provider must be at least 10 years old.");
            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(50).WithMessage("Address cannot exceed 50 characters.");
            RuleFor(c => c.city)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");
            RuleFor(c => c.country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");
        }
    }
}
