using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class Attendance : BaseEntity
    {
        public Guid ChurchId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid MemberId { get; set; }

        public DateTime CheckInTime { get; set; }

        public AttendanceStatus Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double DistanceMeters { get; set; }

        public bool IsWithinGeofence { get; set; }

        public string? DeviceInfo { get; set; }

        public string? IpAddress { get; set; }

        public Service Service { get; set; }
        
        public Member Member { get; set; }
    }
}
