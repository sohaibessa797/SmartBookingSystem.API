using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.ServiceCategory;
using SmartBookingSystem.Application.Interfaces;

namespace SmartBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;
        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Provider + "," + Roles.Customer)]
        public async Task<IActionResult> GetAllServiceCategories()
        {
            try
            {
                var serviceCategories = await _serviceCategoryService.GetAllAsync();
                return Ok(serviceCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("all-with-providers")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Provider + "," + Roles.Customer)]
        public async Task<IActionResult> GetAllServiceCategoriesWithProviders()
        {
            try
            {
                var serviceCategories = await _serviceCategoryService.GetAllWithProvidersAsync();
                return Ok(serviceCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{categoryId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Provider + "," + Roles.Customer)]
        public async Task<IActionResult> GetServiceCategoryById(Guid categoryId)
        {
            try
            {
                var serviceCategory = await _serviceCategoryService.GetByIdAsync(categoryId);
                return Ok(serviceCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("create")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateServiceCategory([FromBody] ServiceCategoryRequest request)
        {
            try
            {
                var serviceCategory = await _serviceCategoryService.CreateAsync(request);
                return CreatedAtAction(nameof(GetServiceCategoryById), new { categoryId = serviceCategory.Id }, serviceCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("update/{categoryId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateServiceCategory(Guid categoryId, [FromBody] ServiceCategoryRequest request)
        {
            try
            {
                var serviceCategory = await _serviceCategoryService.UpdateAsync(categoryId, request);
                return Ok(serviceCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("delete/{categoryId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> SoftDeleteServiceCategory(Guid categoryId)
        {
            try
            {
                var result = await _serviceCategoryService.SoftDeleteServiceCategoryAsync(categoryId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
