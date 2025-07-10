using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.WeeklySchedule;
using SmartBookingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyScheduleController : ControllerBase
    {
        private readonly IWeeklyScheduleService _weeklyScheduleService;
        public WeeklyScheduleController(IWeeklyScheduleService weeklyScheduleService)
        {
            _weeklyScheduleService = weeklyScheduleService;
        }

        [HttpGet("{scheduleId}")]
        [Authorize(Roles = Roles.Admin+","+Roles.Customer)]
        public async Task<IActionResult> GetByIdAsync(Guid scheduleId)
        {
            try
            {
                var schedule = await _weeklyScheduleService.GetByIdAsync(scheduleId);
                return Ok(schedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("current")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> GetCurrentProviderScheduleAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }
            try
            {
                var schedule = await _weeklyScheduleService.GetCurrentProviderScheduleAsync(Guid.Parse(userId));
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("provider/{providerId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Customer)]
        public async Task<IActionResult> GetProviderScheduleAsync(Guid providerId)
        {
            try
            {
                var schedule = await _weeklyScheduleService.GetProviderScheduleAsync(providerId);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("create")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> CreateWeeklyScheduleAsync([FromBody] List<WeeklyScheduleRequest> requests)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");
            
            try
            {
                var schedules = await _weeklyScheduleService.CreateWeeklyScheduleAsync(Guid.Parse(userId), requests);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update/{scheduleId}")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> UpdateAsync(Guid scheduleId, [FromBody] WeeklyScheduleRequest request)
        {
            try
            {
                var updatedSchedule = await _weeklyScheduleService.UpdateAsync(scheduleId, request);
                return Ok(updatedSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("delete/{scheduleId}")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> DeleteAsync(Guid scheduleId)
        {
            try
            {
                var result = await _weeklyScheduleService.DeleteAsync(scheduleId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
