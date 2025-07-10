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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.profilePicture = customer.profilePicture;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.DateOfBirth = customer.DateOfBirth;
                existingCustomer.Address = customer.Address;
                existingCustomer.country = customer.country;
                existingCustomer.city = customer.city;
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Id} not found.");
            }
            await _context.SaveChangesAsync();
        }
    }
}
