using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class UpdateServiceDto
    {
        public string Name { get; set; }
            = string.Empty;

        public ServiceType ServiceType { get; set; }

        public DateTime ServiceDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan LateThreshold { get; set; }

        public TimeSpan AttendanceCloseTime { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int AllowedRadiusMeters { get; set; }

        public bool AttendanceEnabled { get; set; }
    }
}
