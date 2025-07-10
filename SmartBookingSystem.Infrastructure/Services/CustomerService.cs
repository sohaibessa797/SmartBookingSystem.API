using AutoMapper;
using SmartBookingSystem.Application.DTOs.Customer;
using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            if (!customers.Any())
                throw new KeyNotFoundException("No customers found.");
            var customerResponses = _mapper.Map<List<CustomerResponse>>(customers);
            return customerResponses;
        }

        public async Task<CustomerResponse> GetCurrentCustomerProfileAsync(Guid userId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.ApplicationUserId == userId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            return customerResponse;
        }

        public async Task<CustomerResponse> GetCustomerProfileAsync(Guid customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c=>c.Id == customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            return customerResponse;
        }

        public async Task<string> UpdateCurrentCustomerProfileAsync(Guid userId, CustomerRequest request)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.ApplicationUserId == userId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            // Map the request to the customer entity
            _mapper.Map(request, customer);
            // Update the customer in the database
            await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer.Id.ToString();  
        }

        public async Task<string> UpdateCustomerProfileAsync(Guid customerId, CustomerRequest request)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.Id == customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            // Map the request to the customer entity
            _mapper.Map(request, customer);
            // Update the customer in the database
            await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer.Id.ToString();
        }

        public async Task<string> DeleteCustomerProfileAsync(Guid customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(c => c.Id == customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");
            // Soft delete the customer
            await _unitOfWork.Customers.SoftDeleteAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return "Customer deleted successfully.";

        }
    }
}
