using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task ManualCheckInAsync(
    ManualAttendanceDto dto);

        Task SelfCheckInAsync(
            Guid memberId,
            SelfCheckInDto dto);
    
            Task<AttendanceDashboardDto> DashboardAsync();

            Task<List<Attendance>> GetTodayAttendanceAsync();

            Task<List<AttendanceListDto>> GetServiceAttendanceAsync(
                Guid serviceId);

      

            Task<List<AttendanceTrendDto>>
                MonthlyTrendAsync();

            Task<List<DepartmentAttendanceDto>>
                DepartmentAttendanceAsync();

            Task<List<TopAttendeeDto>>
                TopAttendeesAsync();

            Task<List<MemberAttendanceHistoryDto>>
                MemberHistoryAsync(
                    Guid memberId);

            Task<ServiceAttendanceSummaryDto>
                ServiceSummaryAsync(
                    Guid serviceId);
        

        Task<List<Attendance>>
            GetMemberAttendanceAsync(Guid memberId);
    }
}
