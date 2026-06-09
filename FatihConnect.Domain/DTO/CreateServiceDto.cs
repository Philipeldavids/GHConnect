using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public ServiceType ServiceType { get; set; }

        public DateTime ServiceDate { get; set; }

        public string StartTime { get; set; } = string.Empty;

        public string LateThreshold { get; set; } = string.Empty;

        public string AttendanceCloseTime { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int AllowedRadiusMeters { get; set; }
        public bool AttendanceEnabled { get; set; }
    }
}
