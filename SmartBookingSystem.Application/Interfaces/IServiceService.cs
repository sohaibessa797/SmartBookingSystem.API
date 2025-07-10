using SmartBookingSystem.Application.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResponse> GetServiceByIdAsync(Guid serviceId);
        Task<List<ServiceResponse>> GetAllServicesAsync();
        Task<List<ServiceResponse>> GetServicesByProviderIdAsync(Guid providerId);
        Task<List<ServiceResponse>> GetCurrentProviderServicesAsync(Guid userId);
        Task<ServiceResponse> CreateServiceAsync(Guid userId,ServiceRequest request);
        Task<ServiceResponse> UpdateServiceAsync(Guid serviceId, ServiceRequest request);
        Task<string> DeleteServiceAsync(Guid serviceId);
        
    }
}
