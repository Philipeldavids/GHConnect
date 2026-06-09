using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class TodayAttendanceDto
    {
        public Guid MemberId { get; set; }

        public string MembershipNumber { get; set; }

        public string MemberName { get; set; }

        public string Status { get; set; }

        public DateTime CheckInTime { get; set; }
    }
}
