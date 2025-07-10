using SmartBookingSystem.Application.DTOs.Customer;
using SmartBookingSystem.Application.DTOs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAllCustomersAsync();
        Task<CustomerResponse> GetCustomerProfileAsync(Guid customerId);
        Task<CustomerResponse> GetCurrentCustomerProfileAsync(Guid userId);
        Task<string> UpdateCustomerProfileAsync(Guid customerId, CustomerRequest request);
        Task<string> UpdateCurrentCustomerProfileAsync(Guid userId, CustomerRequest request);
        Task<string> DeleteCustomerProfileAsync(Guid customerId);

    }
}
