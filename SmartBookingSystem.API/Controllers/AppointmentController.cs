using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Appointment;
using SmartBookingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        

        [HttpGet("customer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            try
            {
                var appointments = await _appointmentService.GetCustomerAppointmentsAsync(Guid.Parse(userId));
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        

        [HttpGet("provider")]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> GetProviderAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            try
            {
                var appointments = await _appointmentService.GetProviderAppointmentsAsync(Guid.Parse(userId));
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            try
            {
                var appointment = await _appointmentService.CreateAppointmentAsync(Guid.Parse(userId), request);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        

        [HttpPut("{appointmentId}/confirm")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> ConfirmAppointment(Guid appointmentId)
        {
            try
            {
                var result = await _appointmentService.ConfirmAppointmentAsync(appointmentId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{appointmentId}/done")]
        [Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> MarkAppointmentAsDone(Guid appointmentId)
        {
            try
            {
                var result = await _appointmentService.MarkAppointmentAsDoneAsync(appointmentId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{appointmentId}/cancel")]
        [Authorize(Roles = $"{Roles.Provider},{Roles.Customer}")]
        public async Task<IActionResult> CancelAppointment(Guid appointmentId)
        {
            try
            {
                var result = await _appointmentService.CancelAppointmentAsync(appointmentId);
                return Ok(new { message = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{appointmentId}/rate")]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> RateAppointment(Guid appointmentId, [FromBody] AppointmentRatingRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            try
            {
                var result = await _appointmentService.RateAppointmentAsync(Guid.Parse(userId), appointmentId, request);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
