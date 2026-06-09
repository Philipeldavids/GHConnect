using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class DashboardSummaryDto
    {
        public int TotalMembers { get; set; }

        public int PresentToday { get; set; }

        public int LateToday { get; set; }

        public int AbsentToday { get; set; }

        public decimal AttendanceRate { get; set; }

        public int SmsSent { get; set; }

        public int EmailsSent { get; set; }

        public int DepartmentCount { get; set; }

        public int NewMembersThisMonth { get; set; }

        public int BirthdaysThisMonth { get; set; }

        public List<AttendanceTrendDto> AttendanceTrend { get; set; }
            = new();

        public List<TopAttendeeDto> TopAttendees { get; set; }
            = new();
    }
}
