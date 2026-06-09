using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class MemberAttendanceDto
    {
        public Guid AttendanceId { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        public DateTime ServiceDate { get; set; }

        public DateTime CheckInTime { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
