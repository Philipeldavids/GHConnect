using FaithConnect.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
   
        public interface IAttendanceReportService
        {
            Task<AttendanceDashboardDto> DashboardAsync();

            Task<List<AttendanceTrendDto>> MonthlyTrendAsync();

            Task<List<DepartmentAttendanceDto>> DepartmentAttendanceAsync();

            Task<List<TopAttendeeDto>> TopAttendeesAsync();
        }

    }

