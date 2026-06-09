using FaithConnect.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/attendance-reports")]
    [Authorize]
    public class AttendanceReportsController
    : ControllerBase
    {
        private readonly
            IAttendanceReportService
                _service;

        public AttendanceReportsController(
            IAttendanceReportService service)
        {
            _service = service;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
    Dashboard()
        {
            return Ok(
                await _service
                    .DashboardAsync());
        }

        [HttpGet("monthly-trend")]
        public async Task<IActionResult>
    MonthlyTrend()
        {
            return Ok(
                await _service
                    .MonthlyTrendAsync());
        }

        [HttpGet("department-attendance")]
        public async Task<IActionResult>
    DepartmentAttendance()
        {
            return Ok(
                await _service
                    .DepartmentAttendanceAsync());
        }
        [HttpGet("top-attendees")]
        public async Task<IActionResult>
    TopAttendees()
        {
            return Ok(
                await _service
                    .TopAttendeesAsync());
        }
    }
}
