using FaithConnect.Application.Interfaces;
using FaithConnect.Application.Services;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/members")]
    [Authorize]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _service;

        public MembersController(
            IMemberService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(
                await _service.GetDetailsAsync(id)
            );
        }
        [HttpGet("{id}/attendance")]
        public async Task<IActionResult> Attendance(Guid id)
        {
            return Ok(
                await _service.GetAttendanceHistoryAsync(id)
            );
        }

        [HttpGet("{id}/communications")]
        public async Task<IActionResult> Communications(Guid id)
        {
            return Ok(
                await _service.GetCommunicationHistoryAsync(id)
            );
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}/GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateMemberDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateMemberDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
        [HttpPost("bulk-upload")]
        [Authorize(Roles = "ChurchAdmin")]
        public async Task<IActionResult> BulkUpload(
    IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");

            var result =
                await _service.BulkUploadAsync(file);

            return Ok(result);
        }
        [HttpGet("template")]
        public IActionResult DownloadTemplate()
        {
            var path =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Templates",
                    "MemberUploadTemplate.xlsx"
                );

            var bytes =
                System.IO.File.ReadAllBytes(path);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "MemberUploadTemplate.xlsx"
            );
        }
    }
}
