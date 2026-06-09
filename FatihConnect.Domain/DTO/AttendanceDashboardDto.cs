using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
   
    public class AttendanceDashboardDto
    {
        public int TotalMembers { get; set; }

        public int PresentToday { get; set; }

        public int LateToday { get; set; }

        public int AbsentToday { get; set; }

        public double AttendanceRate { get; set; }
    }
}
