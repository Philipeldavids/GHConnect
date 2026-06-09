using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class MemberDashboardDto
    {
        public int ServicesAttended { get; set; }

        public int UpcomingServices { get; set; }

        public double AttendancePercentage { get; set; }

        public string MemberName { get; set; }
            = string.Empty;
    }
}
