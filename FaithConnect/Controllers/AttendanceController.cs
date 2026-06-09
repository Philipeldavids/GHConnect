using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(
            IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // =========================
        // MANUAL CHECK-IN (ADMIN)
        // =========================
        [HttpPost("manual-checkin")]
        public async Task<IActionResult> ManualCheckIn(
            [FromBody] ManualAttendanceDto dto)
        {
            try
            {
                await _attendanceService
                    .ManualCheckInAsync(dto);

                return Ok(new
                {
                    message = "Attendance recorded successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // =========================
        // SELF CHECK-IN (MEMBER)
        // =========================
        [HttpPost("self-checkin")]
        public async Task<IActionResult> SelfCheckIn(
            
            [FromBody] SelfCheckInDto dto)
        {
            var memberId = Guid.Parse(User.FindFirst("MemberId")!.Value);
            try
            {
                await _attendanceService
                    .SelfCheckInAsync(memberId, dto);

                return Ok(new
                {
                    message = "Check-in successful"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // =========================
        // TODAY ATTENDANCE
        // =========================
        [HttpGet("today")]
        public async Task<IActionResult> Today()
        {
            var result =
                await _attendanceService
                    .GetTodayAttendanceAsync();

            return Ok(result);
        }

        // =========================
        // ATTENDANCE DASHBOARD
        // =========================
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var result =
                await _attendanceService
                    .DashboardAsync();

            return Ok(result);
        }

        // =========================
        // MONTHLY TREND
        // =========================
        [HttpGet("trend/monthly")]
        public async Task<IActionResult> MonthlyTrend()
        {
            var result =
                await _attendanceService
                    .MonthlyTrendAsync();

            return Ok(result);
        }

        // =========================
        // DEPARTMENT ATTENDANCE
        // =========================
        [HttpGet("departments")]
        public async Task<IActionResult> DepartmentAttendance()
        {
            var result =
                await _attendanceService
                    .DepartmentAttendanceAsync();

            return Ok(result);
        }

        // =========================
        // TOP ATTENDEES
        // =========================
        [HttpGet("top-attendees")]
        public async Task<IActionResult> TopAttendees()
        {
            var result =
                await _attendanceService
                    .TopAttendeesAsync();

            return Ok(result);
        }

        // =========================
        // MEMBER HISTORY
        // =========================
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> MemberHistory(Guid memberId)
        {
            var result =
                await _attendanceService
                    .MemberHistoryAsync(memberId);

            return Ok(result);
        }
        [HttpGet("service/{serviceId}")]
        public async Task<IActionResult>
GetServiceAttendance(Guid serviceId)
        {
            var result =
                await _attendanceService
                    .GetServiceAttendanceAsync(
                        serviceId);

            return Ok(result);
        }
        // =========================
        // SERVICE SUMMARY
        // =========================


        [HttpGet("service/{serviceId}/summary")]
        public async Task<IActionResult> ServiceSummary(Guid serviceId)
        {
            var result =
                await _attendanceService
                    .ServiceSummaryAsync(serviceId);

            return Ok(result);
        }

        // =========================
        // DELETE (optional admin)
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // If you later add DeleteAttendanceAsync to service
            // keep controller clean
            return Ok();
        }
    }
}