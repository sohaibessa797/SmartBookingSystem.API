using FluentValidation;
using SmartBookingSystem.Application.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.Service
{
    public class ServiceValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceValidator() 
        {
            RuleFor(service => service.Name)
                .NotEmpty().WithMessage("Service name is required.")
                .MaximumLength(50).WithMessage("Service name must not exceed 50 characters.");
            RuleFor(service => service.Description)
                .NotEmpty().WithMessage("Service description is required.")
                .MaximumLength(500).WithMessage("Service description must not exceed 500 characters.")
                .Must(desc => !desc.Contains("<") && !desc.Contains(">"))
                .WithMessage("Description should not contain HTML tags.");
            RuleFor(service => service.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Service price must be a positive number.");
            RuleFor(service => service.ServiceCategoryId)
                .NotEmpty().WithMessage("Service category ID is required.");
        }
    }
}
