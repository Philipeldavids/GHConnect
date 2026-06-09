using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public class PermissionsController
    : ControllerBase
    {
        private readonly IPermissionService
            _permissionService;

        public PermissionsController(
            IPermissionService
                permissionService)
        {
            _permissionService =
                permissionService;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            return Ok(
                await _permissionService
                    .GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreatePermissionDto dto)
        {
            return Ok(
                await _permissionService
                    .CreateAsync(dto));
        }

        [HttpPost("assign")]
        public async Task<IActionResult>
            AssignPermissions(
                AssignPermissionDto dto)
        {
            await _permissionService
                .AssignPermissionsAsync(
                    dto);

            return NoContent();
        }

        [HttpGet("role/{roleId}")]
        public async Task<IActionResult>
            GetRolePermissions(
                string roleId)
        {
            return Ok(
                await _permissionService
                    .GetRolePermissionsAsync(
                        roleId));
        }
    }
}
