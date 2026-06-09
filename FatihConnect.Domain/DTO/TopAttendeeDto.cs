using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class TopAttendeeDto
    {
        public Guid MemberId { get; set; }

        public string MembershipNumber { get; set; } = string.Empty;

        public string MemberName { get; set; } = string.Empty;

        public int AttendanceCount { get; set; }
    }
}
