using AutoMapper;
using Azure.Core;
using SmartBookingSystem.Application.DTOs.Service;
using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ServiceResponse>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.Services.GetAllAsync(null,x=>x.Provider,x=>x.ServiceCategory);
            if (!services.Any())
                throw new KeyNotFoundException("No services found.");
            var serviceResponses = _mapper.Map<List<ServiceResponse>>(services);
            return serviceResponses;
        }

        public async Task<ServiceResponse> GetServiceByIdAsync(Guid serviceId)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(s=> s.Id == serviceId, x => x.Provider, x => x.ServiceCategory);
            if (service == null)
                throw new KeyNotFoundException("Service not found.");
            var serviceResponse = _mapper.Map<ServiceResponse>(service);
            return serviceResponse;

        }

        public async Task<List<ServiceResponse>> GetServicesByProviderIdAsync(Guid providerId)
        {
            var services = await _unitOfWork.Services.GetAllAsync(s => s.ProviderId == providerId, x => x.Provider, x => x.ServiceCategory);
            if (!services.Any())
                throw new KeyNotFoundException("No services found for this provider.");
            var serviceResponses = _mapper.Map<List<ServiceResponse>>(services);
            return serviceResponses;
        }
        public async Task<List<ServiceResponse>> GetCurrentProviderServicesAsync(Guid userId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found for the current user.");
            var services = await _unitOfWork.Services.GetAllAsync(s => s.ProviderId == provider.Id, x => x.Provider, x => x.ServiceCategory);
            if (!services.Any())
                throw new KeyNotFoundException("No services found for this provider.");
            var serviceResponses = _mapper.Map<List<ServiceResponse>>(services);
            return serviceResponses;
        }

        public async Task<ServiceResponse> CreateServiceAsync(Guid userId,ServiceRequest request)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found for the current user.");
            var exists = await _unitOfWork.Services.AnyAsync(c => c.Name == request.Name);
            if (exists)
                throw new InvalidOperationException("A service with the same name already exists.");
            var service = _mapper.Map<Service>(request);
            service.ProviderId = provider.Id;
            await _unitOfWork.Services.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();

            var serviceWithDetails = await _unitOfWork.Services.GetByIdAsync(
                s => s.Id == service.Id,
                x => x.Provider,
                x => x.ServiceCategory);
            var serviceResponse = _mapper.Map<ServiceResponse>(serviceWithDetails);

            return serviceResponse;
        }

        public async Task<ServiceResponse> UpdateServiceAsync(Guid serviceId, ServiceRequest request)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(s => s.Id == serviceId);
            if (service == null)
                throw new KeyNotFoundException("Service not found.");
            var exists = await _unitOfWork.Services.AnyAsync(c => c.Name == request.Name && c.Id != serviceId);
            if (exists)
                throw new InvalidOperationException("A service category with the same name already exists.");
            _mapper.Map(request, service);
            await _unitOfWork.Services.UpdateAsync(service);
            await _unitOfWork.SaveChangesAsync();

            var serviceWithDetails = await _unitOfWork.Services.GetByIdAsync(
                            s => s.Id == service.Id,
                            x => x.Provider,
                            x => x.ServiceCategory);
            var serviceResponse = _mapper.Map<ServiceResponse>(serviceWithDetails);
            return serviceResponse;
        }
        public async Task<string> DeleteServiceAsync(Guid serviceId)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(s => s.Id == serviceId);
            if (service == null)
                throw new KeyNotFoundException("Service not found.");
            await _unitOfWork.Services.SoftDeleteAsync(service);
            await _unitOfWork.SaveChangesAsync();
            return ("Service deleted successfully.");
        }

    }
}
