using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Service;
using SmartBookingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin+","+Roles.Customer)]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                var services = await _serviceService.GetAllServicesAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{serviceId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Customer)]
        public async Task<IActionResult> GetServiceById(Guid serviceId)
        {
            try
            {
                var service = await _serviceService.GetServiceByIdAsync(serviceId);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("provider/{providerId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Customer)]
        public async Task<IActionResult> GetServicesByProviderId(Guid providerId)
        {
            try
            {
                var services = await _serviceService.GetServicesByProviderIdAsync(providerId);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("current-provider")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> GetCurrentProviderServices()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { message = "User not authenticated." });
                var services = await _serviceService.GetCurrentProviderServicesAsync(Guid.Parse(userId));
                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { message = "User not authenticated." });
                var service = await _serviceService.CreateServiceAsync(Guid.Parse(userId), request);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{serviceId}")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> UpdateService(Guid serviceId, [FromBody] ServiceRequest request)
        {
            try
            {
                var service = await _serviceService.UpdateServiceAsync(serviceId, request);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{serviceId}")]
        [Authorize(Roles = Roles.Provider+","+Roles.Admin)]
        public async Task<IActionResult> DeleteService(Guid serviceId)
        {
            try
            {
                var result = await _serviceService.DeleteServiceAsync(serviceId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
