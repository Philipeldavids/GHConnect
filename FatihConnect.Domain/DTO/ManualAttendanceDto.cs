using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class ManualAttendanceDto
    {
        public Guid ServiceId { get; set; }

        public Guid MemberId { get; set; }

        public AttendanceStatus Status { get; set; }
    }
}
