using FluentValidation;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.Account
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MinimumLength(3).WithMessage("First Name must be at least 3 characters long.");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters long.");
           
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invaild Email Format");
           
            RuleFor(x => x.ConfirmEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invaild Email Format")
                .Equal(x => x.Email).WithMessage("Emails must match.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
               

            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required.")
                .Must(role => role == Roles.Provider || role == Roles.Customer )
                .WithMessage("Role must be either 'Provider' or 'Customer'.");
        }
    }
}
