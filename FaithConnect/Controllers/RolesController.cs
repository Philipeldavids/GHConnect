using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController
    : ControllerBase
    {
        private readonly IRoleService
            _roleService;

        public RolesController(
            IRoleService roleService)
        {
            _roleService =
                roleService;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            return Ok(
                await _roleService
                    .GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(
                string id)
        {
            return Ok(
                await _roleService
                    .GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreateRoleDto dto)
        {
            return Ok(
                await _roleService
                    .CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(
                string id,
                UpdateRoleDto dto)
        {
            await _roleService
                .UpdateAsync(
                    id,
                    dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(
                string id)
        {
            await _roleService
                .DeleteAsync(id);

            return NoContent();
        }
    }
}
