using FluentValidation;
using SmartBookingSystem.Application.DTOs.ServiceCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.ServiceCategory
{
    public class ServiceCategoryValidator : AbstractValidator<ServiceCategoryRequest>
    {
        public ServiceCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .Must(desc => !desc.Contains("<") && !desc.Contains(">"))
                .WithMessage("Description should not contain HTML tags.");
        }
    }
}
