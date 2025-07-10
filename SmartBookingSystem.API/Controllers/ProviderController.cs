using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Provider;
using SmartBookingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;
        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }
        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin+","+Roles.Customer)]
        public async Task<IActionResult> GetAllProviders()
        {
            try
            {
                var providers = await _providerService.GetAllProvidersAsync();
                return Ok(providers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpGet("profile")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> GetCurrentProviderProfile()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { message = "User not authenticated." });
                var providerProfile = await _providerService.GetCurrentProviderProfileAsync(Guid.Parse(userId));
                return Ok(providerProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{providerId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Customer)]
        public async Task<IActionResult> GetProviderDetails(Guid providerId)
        {
            try
            {
                var providerDetails = await _providerService.GetProviderDetailsAsync(providerId);
                return Ok(providerDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPut("update/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateProviderProfile(Guid id, [FromBody] ProviderRequest request)
        {
            try
            {
                var response = await _providerService.UpdateProviderAsync(id, request);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("me/update")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> UpdateCurrentProviderProfile([FromBody] ProviderRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized(new { message = "User not authenticated." });

                var response = await _providerService.UpdateCurrentProviderProfileAsync(Guid.Parse(userId), request);
                return Ok(new { message = "Profile updated", id = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{providerId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> SoftDeleteProvider(Guid providerId)
        {
            try
            {
                var response = await _providerService.SoftDeleteProviderAsync(providerId);
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
