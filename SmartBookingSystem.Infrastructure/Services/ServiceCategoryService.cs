using AutoMapper;
using SmartBookingSystem.Application.DTOs.Provider;
using SmartBookingSystem.Application.DTOs.ServiceCategory;
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
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<ServiceCategoryResponse>> GetAllAsync()
        {
            var categories = await _unitOfWork.ServiceCategories.GetAllAsync();
            if (!categories.Any())
                throw new KeyNotFoundException("No service categories found.");
            var categoryResponses = _mapper.Map<List<ServiceCategoryResponse>>(categories);
            return categoryResponses;
        }

        public async Task<List<ServiceCategoryWithProviderResponse>> GetAllWithProvidersAsync()
        {
            var categories = await _unitOfWork.ServiceCategories.GetAllAsync(null,p=>p.Providers);
            if (!categories.Any())
                throw new KeyNotFoundException("No service categories found.");
            
            var categoryResponses = categories.Select(c => new ServiceCategoryWithProviderResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Providers = c.Providers.Select(p => new ProviderListItemResponse
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}",
                    ProfilePicture = p.ProfilePicture,
                    ShortDescription = p.Description?.Length > 100 ? p.Description.Substring(0, 100) + "..." : p.Description,
                    ServiceCategoryName = c.Name
                }).ToList()
            }).ToList();
            return categoryResponses;
        }

        public async Task<ServiceCategoryResponse> GetByIdAsync(Guid categoryId)
        {
            var category = await _unitOfWork.ServiceCategories.GetByIdAsync(c => c.Id == categoryId);
            if (category == null)
                throw new KeyNotFoundException("Service category not found.");
            var categoryResponse = _mapper.Map<ServiceCategoryResponse>(category);
            return categoryResponse;
        }

        public async Task<ServiceCategoryResponse> CreateAsync(ServiceCategoryRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Service category request cannot be null.");
            var exists = await _unitOfWork.ServiceCategories.AnyAsync(c => c.Name == request.Name);
            if (exists)
                throw new InvalidOperationException("A service category with the same name already exists.");
            var category = _mapper.Map<ServiceCategory>(request);
            await _unitOfWork.ServiceCategories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            var categoryResponse = _mapper.Map<ServiceCategoryResponse>(category);
            return categoryResponse;
        }


        public async Task<ServiceCategoryResponse> UpdateAsync(Guid categoryId, ServiceCategoryRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Service category request cannot be null.");
            var category = await _unitOfWork.ServiceCategories.GetByIdAsync(c => c.Id == categoryId);
            if (category == null)
                throw new KeyNotFoundException("Service category not found.");
            _mapper.Map(request, category);
            await _unitOfWork.ServiceCategories.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();
            var categoryResponse = _mapper.Map<ServiceCategoryResponse>(category);
            return categoryResponse;
        }

        public async Task<string> SoftDeleteServiceCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.ServiceCategories.GetByIdAsync(c => c.Id == categoryId);
            if (category == null)
                throw new KeyNotFoundException("Service category not found.");
            await _unitOfWork.ServiceCategories.SoftDeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return "Service category deleted successfully.";
        }
    }
}
