using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Provider;
using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProviderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProviderListItemResponse>> GetAllProvidersAsync()
        {
            var providers = await _unitOfWork.Providers.GetAllAsync(null, c => c.ServiceCategory);

            if (!providers.Any())
                throw new KeyNotFoundException("No providers found.");

            var result = providers.Select(p => new ProviderListItemResponse
            {
                Id = p.Id,
                Name = $"{p.FirstName} {p.LastName}",
                ProfilePicture = p.ProfilePicture,
                ShortDescription = p.Description?.Length > 100 ? p.Description.Substring(0, 100) + "..." : p.Description,
                ServiceCategoryName = p.ServiceCategory?.Name ?? "Uncategorized",
                AverageRating = 0 
            }).ToList();

            return result;
        }


        public async Task<ProviderResponse> GetCurrentProviderProfileAsync(Guid userId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(
                e => e.ApplicationUserId == userId,
                c => c.ServiceCategory);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");
            var providerResponse = _mapper.Map<ProviderResponse>(provider);
            return providerResponse;
        }

        public async Task<ProviderResponse> GetProviderDetailsAsync(Guid providerId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(
                e => e.Id == providerId,
                c => c.ServiceCategory);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");
            var providerResponse = _mapper.Map<ProviderResponse>(provider);
            return providerResponse;
        }

        public async Task<string> UpdateProviderAsync(Guid Id,ProviderRequest request)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(
                e => e.Id == Id,
                c => c.ServiceCategory);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");
            _mapper.Map(request, provider);
            await _unitOfWork.Providers.UpdateAsync(provider);
            await _unitOfWork.SaveChangesAsync();
            return provider.Id.ToString();
        }
        public async Task<string> UpdateCurrentProviderProfileAsync(Guid userId, ProviderRequest request)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(
                e => e.ApplicationUserId == userId,
                c => c.ServiceCategory);

            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");

            _mapper.Map(request, provider);

            await _unitOfWork.Providers.UpdateAsync(provider);
            await _unitOfWork.SaveChangesAsync();

            return provider.Id.ToString();
        }

        public async Task<string> SoftDeleteProviderAsync(Guid providerId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(d => d.Id == providerId);
            if (provider == null)
            {
                throw new KeyNotFoundException("Provider not found.");
            }
            await _unitOfWork.Providers.SoftDeleteAsync(provider);
            await _unitOfWork.SaveChangesAsync();
            return "Provider deleted successfully.";
        }
    }
}
