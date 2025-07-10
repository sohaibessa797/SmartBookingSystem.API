using FluentValidation;
using SmartBookingSystem.Application.DTOs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Validators.Provider
{
    public class ProviderValidator : AbstractValidator<ProviderRequest>
    {
        public ProviderValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .Must(desc => !desc.Contains("<") && !desc.Contains(">"))
                .WithMessage("Description should not contain HTML tags.");
            RuleFor(x => x.ProfilePicture)
                .Matches(@"^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w.-]*)*/?$")
                .When(x => !string.IsNullOrEmpty(x.ProfilePicture))
                .WithMessage("Invalid profile picture URL.");
            RuleFor(x => x.CoverImageUrl)
                .Matches(@"^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w.-]*)*/?$")
                .When(x => !string.IsNullOrEmpty(x.CoverImageUrl))
                .WithMessage("Invalid cover image URL.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.")
                .Must(date => date <= DateTime.Now.AddYears(-18))
                .WithMessage("Provider must be at least 18 years old."); 
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(50).WithMessage("Address must not exceed 50 characters.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");
            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90.0, 90.0).WithMessage("Latitude must be between -90 and 90 degrees.");
            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180.0, 180.0).WithMessage("Longitude must be between -180 and 180 degrees.");
            RuleFor(x => x.WorkingSince)
                .GreaterThanOrEqualTo(1900).WithMessage("Working since year must be greater than or equal to 1900.");
            
            RuleFor(x => x.WebsiteUrl)
                .Matches(@"^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w.-]*)*/?$").When(x => !string.IsNullOrEmpty(x.WebsiteUrl))
                .WithMessage("Invalid website URL format.");
            RuleFor(x => x.FacebookPage)
                .Matches(@"^(https?://)?(www\.)?facebook\.com/[\w.-]+$").When(x => !string.IsNullOrEmpty(x.FacebookPage))
                .WithMessage("Invalid Facebook page URL format.");
            RuleFor(x => x.InstagramHandle)
                .Matches(@"^(https?://)?(www\.)?instagram\.com/[\w.-]+$").When(x => !string.IsNullOrEmpty(x.InstagramHandle))
                .WithMessage("Invalid Instagram handle URL format.");

        }
    }
}
