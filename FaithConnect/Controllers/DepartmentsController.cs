using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(
                await _departmentService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(
    CreateDepartmentDto dto)
        {
            await _departmentService.CreateAsync(dto);

            return Ok();
        }

        [HttpDelete("{departmentId}/members/{memberId}")]
        public async Task<IActionResult> RemoveMember(
    Guid departmentId,
    Guid memberId)
        {
            await _departmentService.RemoveMemberAsync(
                memberId,
                departmentId);

            return Ok();
        }
        [HttpPost("assign-member")]
        public async Task<IActionResult> AssignMember(
    AssignMemberDepartmentDto dto)
        {
            await _departmentService.AssignMemberAsync(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _departmentService.DeleteAsync(id);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
    Guid id,
    UpdateDepartmentDto dto)
        {
            await _departmentService.UpdateAsync(
                id,
                dto);

            return Ok();
        }
    }
}
