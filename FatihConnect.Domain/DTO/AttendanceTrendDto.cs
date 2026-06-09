using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class AttendanceTrendDto
    {
        public string Period { get; set; } = string.Empty;

        public int AttendanceCount { get; set; }
    }
}
