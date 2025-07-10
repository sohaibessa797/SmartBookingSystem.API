using SmartBookingSystem.Application.DTOs.ServiceCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface IServiceCategoryService
    {
        Task<List<ServiceCategoryResponse>> GetAllAsync();
        Task<List<ServiceCategoryWithProviderResponse>> GetAllWithProvidersAsync();
        Task<ServiceCategoryResponse> GetByIdAsync(Guid categoryId);
        Task<ServiceCategoryResponse> CreateAsync(ServiceCategoryRequest request);
        Task<ServiceCategoryResponse> UpdateAsync(Guid categoryId, ServiceCategoryRequest request);
        Task<string> SoftDeleteServiceCategoryAsync(Guid categoryId);
    }
}
