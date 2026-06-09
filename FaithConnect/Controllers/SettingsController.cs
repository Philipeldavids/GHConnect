using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{


    [Authorize]
    [ApiController]
    [Route("api/settings")]
    public class SettingsController
        : ControllerBase
    {
        private readonly ISettingsService
            _service;

        public SettingsController(
            ISettingsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult>
            Get()
        {
            return Ok(
                await _service.GetAsync());
        }

        [HttpPut]
        public async Task<IActionResult>
            Update(
                [FromBody]
            UpdateChurchSettingsDto dto)
        {
            await _service.UpdateAsync(dto);

            return Ok(
                "Settings updated");
        }
    }
}
