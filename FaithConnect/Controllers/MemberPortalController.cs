using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/memberportal")]
    public class MemberPortalController
    : ControllerBase
    {
        private readonly
            IMemberPortalService
            _service;

        private readonly
            ITenantService
            _tenantService;

        public MemberPortalController(
            IMemberPortalService service,
            ITenantService tenantService)
        {
            _service = service;
            _tenantService =
                tenantService;
        }

        [HttpGet("dashboard")]
        [Authorize]
        public async Task<IActionResult>
            Dashboard()
        {
            var memberClaim =
    User.FindFirst("MemberId");

            if (memberClaim == null)
            {
                return Unauthorized(
                    "MemberId claim not found.");
            }

            if (!Guid.TryParse(
                    memberClaim.Value,
                    out var memberId))
            {
                return BadRequest(
                    "Invalid MemberId.");
            }

            return Ok(
                await _service
                    .DashboardAsync(
                        memberId));
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult>
            Profile()
        {
            var memberClaim =
     User.FindFirst("MemberId");

            if (memberClaim == null)
            {
                return Unauthorized(
                    "MemberId claim not found.");
            }

            if (!Guid.TryParse(
                    memberClaim.Value,
                    out var memberId))
            {
                return BadRequest(
                    "Invalid MemberId.");
            }

            return Ok(
                await _service
                    .ProfileAsync(
                        memberId));
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult>
            UpdateProfile(
                UpdateMemberProfileDto dto)
        {
            var memberClaim =
     User.FindFirst("MemberId");

            if (memberClaim == null)
            {
                return Unauthorized(
                    "MemberId claim not found.");
            }

            if (!Guid.TryParse(
                    memberClaim.Value,
                    out var memberId))
            {
                return BadRequest(
                    "Invalid MemberId.");
            }

            await _service
                .UpdateProfileAsync(
                    memberId,
                    dto);

            return Ok();
        }

        [HttpGet("attendance")]
        [Authorize]
        public async Task<IActionResult>
            Attendance()
        {
            var memberClaim =
    User.FindFirst("MemberId");

            if (memberClaim == null)
            {
                return Unauthorized(
                    "MemberId claim not found.");
            }

            if (!Guid.TryParse(
                    memberClaim.Value,
                    out var memberId))
            {
                return BadRequest(
                    "Invalid MemberId.");
            }
            return Ok(
                await _service
                    .AttendanceAsync(
                        memberId));
        }

        [HttpGet("services")]
        [Authorize]
        public async Task<IActionResult>
            Services()
        {
            return Ok(
                await _service
                    .UpcomingServicesAsync(
                        _tenantService
                            .GetTenantId()));
        }
    }
}
