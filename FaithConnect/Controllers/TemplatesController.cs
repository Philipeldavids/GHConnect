using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/templates")]
    public class TemplatesController
    : ControllerBase
    {
        private readonly ITemplateService _service;

        public TemplatesController(
            ITemplateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            return Ok(
                await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            Get(Guid id)
        {
            return Ok(
                await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreateTemplateDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(
                Guid id,
                UpdateTemplateDto dto)
        {
            await _service.UpdateAsync(
                id,
                dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
