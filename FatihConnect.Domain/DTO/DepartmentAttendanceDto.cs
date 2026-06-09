using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class DepartmentAttendanceDto
    {
        public string DepartmentName { get; set; } = string.Empty;

        public int AttendanceCount { get; set; }
    }
}
