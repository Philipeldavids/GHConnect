using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/services")]
    [Authorize]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(
            IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(
            Guid id)
        {
            var result =
                await _service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult>
            Upcoming()
        {
            var result =
                await _service
                    .UpcomingServicesAsync();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(
            Roles =
                "SuperAdmin,ChurchAdmin")]
        public async Task<IActionResult>
            Create(
                [FromBody]
            CreateServiceDto dto)
        {
            await _service.CreateAsync(
                dto);

            return Ok(
                new
                {
                    message =
                        "Service created successfully"
                });
        }

        [HttpPut("{id:guid}")]
        [Authorize(
            Roles =
                "SuperAdmin,ChurchAdmin")]
        public async Task<IActionResult>
            Update(
                Guid id,
                [FromBody]
            UpdateServiceDto dto)
        {
            await _service.UpdateAsync(
                id,
                dto);

            return Ok(
                new
                {
                    message =
                        "Service updated successfully"
                });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(
            Roles =
                "SuperAdmin,ChurchAdmin")]
        public async Task<IActionResult>
            Delete(
                Guid id)
        {
            await _service.DeleteAsync(
                id);

            return Ok(
                new
                {
                    message =
                        "Service deleted successfully"
                });
        }
    }
}
