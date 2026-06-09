using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class MemberAttendanceHistoryDto
    {
        public Guid AttendanceId { get; set; }

        public string ServiceName { get; set; }
            = string.Empty;

        public DateTime ServiceDate { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public DateTime CheckInTime { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double DistanceMeters { get; set; }

        public bool IsWithinGeofence { get; set; }
    }
}
