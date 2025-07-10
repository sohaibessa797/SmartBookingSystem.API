using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using SmartBookingSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(Provider provider)
        {
            var existingProvider = await _context.Providers.FindAsync(provider.Id);
            if (existingProvider != null)
            {
                existingProvider.FirstName = provider.FirstName;
                existingProvider.LastName = provider.LastName;
                existingProvider.Description = provider.Description;
                existingProvider.PhoneNumber = provider.PhoneNumber;
                existingProvider.ProfilePicture = provider.ProfilePicture;
                existingProvider.Address = provider.Address;
                existingProvider.CoverImageUrl = provider.CoverImageUrl;
                existingProvider.Country = provider.Country;
                existingProvider.City = provider.City;
                existingProvider.DateOfBirth = provider.DateOfBirth;
                existingProvider.Latitude = provider.Latitude;
                existingProvider.Longitude = provider.Longitude;
                existingProvider.WorkingSince = provider.WorkingSince;
                existingProvider.WebsiteUrl = provider.WebsiteUrl;
                existingProvider.FacebookPage = provider.FacebookPage;
                existingProvider.InstagramHandle = provider.InstagramHandle;
                existingProvider.IsApproved = provider.IsApproved;
                existingProvider.AverageRating = provider.AverageRating;
            }
            else
            {
                throw new KeyNotFoundException($"Provider with ID {provider.Id} not found.");
            }
            await _context.SaveChangesAsync();
        }
    }
}
