using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class Church : BaseEntity
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
        public string LogoUrl { get; set; }
            = string.Empty;

        public int LastMemberNumber { get; set; }

        // Attendance / Geofencing

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int DefaultAllowedRadiusMeters
        {
            get;
            set;
        } = 100;

        // Navigation Properties

        public ICollection<Member> Members
        {
            get;
            set;
        }
            = new List<Member>();
    }
}
