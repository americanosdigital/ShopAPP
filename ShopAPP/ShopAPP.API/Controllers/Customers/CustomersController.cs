using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPP.API.Controllers.Orders;
using ShopAPP.Application.DTOs.Customers;
using ShopAPP.Application.Interfaces.Customers;

namespace ShopAPP.API.Controllers.Customers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService service, ILogger<CustomersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await _service.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all customers.");
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var customer = await _service.GetByIdAsync(id);
                if (customer == null) return NotFound("Customer not found.");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer by ID.");
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CustomerCreateDto dto)
        {
            try
            {
                var imageUrl = await SaveImageAsync(dto.ImageFile, "customers");
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    dto.ImageFile = null;
                    dto.ImageUrl = imageUrl;
                }

                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer.");
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CustomerUpdateDto dto)
        {
            try
            {
                var imageUrl = await SaveImageAsync(dto.ImageFile, "customers");
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    dto.ImageFile = null;
                    dto.ImageUrl = imageUrl;
                }

                await _service.UpdateAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer.");
                return BadRequest(new { ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer.");
                return BadRequest(new { ex.Message });
            }
        }

        private async Task<string?> SaveImageAsync(IFormFile? imageFile, string folder)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{uniqueFileName}";
        }

    }

}
