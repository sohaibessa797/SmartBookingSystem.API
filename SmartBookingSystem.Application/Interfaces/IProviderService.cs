using SmartBookingSystem.Application.DTOs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface IProviderService
    {
        Task<string> UpdateProviderAsync(Guid Id,ProviderRequest request);
        Task<string> SoftDeleteProviderAsync(Guid providerId);
        Task<ProviderResponse> GetProviderDetailsAsync(Guid providerId);
        Task<ProviderResponse> GetCurrentProviderProfileAsync(Guid userId);
        Task<List<ProviderListItemResponse>> GetAllProvidersAsync();
        Task<string> UpdateCurrentProviderProfileAsync(Guid userId, ProviderRequest request);


    }
}
