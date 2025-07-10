using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Customer;
using SmartBookingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> GetCurrentCustomerProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { message = "User not authenticated." });
                var customerProfile = await _customerService.GetCurrentCustomerProfileAsync(Guid.Parse(userId));
                return Ok(customerProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{customerId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetCustomerProfile(Guid customerId)
        {
            try
            {
                var customerProfile = await _customerService.GetCustomerProfileAsync(customerId);
                return Ok(customerProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update/{customerId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateCustomerProfile(Guid customerId, [FromBody] CustomerRequest request)
        {
            try
            {
                var result = await _customerService.UpdateCustomerProfileAsync(customerId, request);
                return Ok(new { message = "Profile updated successfully.", customerId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("me/update")]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> UpdateCurrentCustomerProfile([FromBody] CustomerRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { message = "User not authenticated." });
                var result = await _customerService.UpdateCurrentCustomerProfileAsync(Guid.Parse(userId), request);
                return Ok(new { message = "Profile updated successfully.", customerId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpDelete("delete/{customerId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteCustomerProfile(Guid customerId)
        {
            try
            {
                var result = await _customerService.DeleteCustomerProfileAsync(customerId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
