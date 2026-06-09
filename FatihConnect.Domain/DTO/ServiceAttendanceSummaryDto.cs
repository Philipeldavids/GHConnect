using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class ServiceAttendanceSummaryDto
    {
        public Guid ServiceId { get; set; }

        public string ServiceName { get; set; }
            = string.Empty;

        public int Present { get; set; }

        public int Late { get; set; }

        public int Absent { get; set; }

        public int TotalAttendance { get; set; }
    }
}
