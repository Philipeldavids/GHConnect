using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class CreateMemberDto
    {
        public string FirstName { get; set; }
            = string.Empty;

        public string LastName { get; set; }
            = string.Empty;

        public string OtherName { get; set; }
            = string.Empty;

        public string Gender { get; set; }
            = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string MaritalStatus { get; set; }
            = string.Empty;

        public string PhoneNumber { get; set; }
            = string.Empty;

        public string Email { get; set; }
            = string.Empty;

        public string Occupation { get; set; }
            = string.Empty;

        public string Address { get; set; }
            = string.Empty;
    }
}
