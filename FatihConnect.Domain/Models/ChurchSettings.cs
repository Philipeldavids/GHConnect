using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class ChurchSettings : BaseEntity
    {
        public Guid TenantId { get; set; }

        public string ChurchName { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        public string EmailAddress { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public double AttendanceRadiusMeters { get; set; }

        public string SmsProvider { get; set; } = string.Empty;

        public string EmailProvider { get; set; } = string.Empty;
    }
}
