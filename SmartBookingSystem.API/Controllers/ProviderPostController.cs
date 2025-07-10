using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.ProviderPost;
using SmartBookingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderPostController : ControllerBase
    {
        private readonly IProviderPostService _providerPostService;
        public ProviderPostController(IProviderPostService providerPostService)
        {
            _providerPostService = providerPostService;
        }

        [HttpGet("{postId}")]
        [Authorize(Roles =$"{Roles.Customer},{Roles.Admin},{Roles.Provider}")]
        public async Task<IActionResult> GetByIdAsync(Guid postId)
        {
            try
            {
                var response = await _providerPostService.GetByIdAsync(postId);
                return Ok(response);
            }
            catch(Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("provider/{providerId}")]
        [Authorize(Roles = $"{Roles.Customer},{Roles.Admin}")]
        public async Task<IActionResult> GetAllByProviderAsync(Guid providerId)
        {
            try
            {
                var posts = _providerPostService.GetAllByProviderAsync(providerId);
                return Ok(posts);
            }
            catch(Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("current")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> GetCurrentProviderPosts()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User not authenticated.");

                var posts = await _providerPostService.GetCurrentProviderPostAsync(Guid.Parse(userId));
                return Ok(posts);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpPost("create")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> CreateAsync([FromForm] ProviderPostRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User not authenticated.");
                var response = await _providerPostService.CreateAsync(Guid.Parse(userId),request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{postId}")]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> UpdateAsync(Guid postId, [FromForm] ProviderPostRequest request)
        {
            try
            {
                var result = await _providerPostService.UpdateAsync(postId, request);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{postId}")]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> DeleteAsync(Guid postId)
        {
            try
            {
                var result = await _providerPostService.DeleteAsync(postId);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
