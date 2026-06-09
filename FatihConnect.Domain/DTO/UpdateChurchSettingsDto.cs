using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class UpdateChurchSettingsDto
    {
        public string Name { get; set; }
            = string.Empty;

        public string Email { get; set; }
            = string.Empty;

        public string PhoneNumber { get; set; }
            = string.Empty;

        public string Address { get; set; }
            = string.Empty;

        public string Website { get; set; }
            = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int DefaultAllowedRadiusMeters { get; set; }

        public string LogoUrl { get; set; }
            = string.Empty;
    }
}
