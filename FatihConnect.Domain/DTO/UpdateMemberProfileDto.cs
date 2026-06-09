using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class UpdateMemberProfileDto
    {
        public string PhoneNumber { get; set; }
            = string.Empty;

        public string Email { get; set; }
            = string.Empty;

        public string Address { get; set; }
            = string.Empty;

        public string Occupation { get; set; }
            = string.Empty;

        public string EmergencyContactName { get; set; }
            = string.Empty;

        public string EmergencyContactPhone { get; set; }
            = string.Empty;
    }
}
