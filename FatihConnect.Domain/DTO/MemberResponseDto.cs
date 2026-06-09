using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class MemberResponseDto
    {
        public Guid Id { get; set; }

        public string MembershipNumber { get; set; }
            = string.Empty;

        public string FirstName { get; set; }
            = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
            = string.Empty;

        public string Email { get; set; }
            = string.Empty;

        public DateTime MembershipDate { get; set; }

        public bool IsActive { get; set; }
    }
}
