using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class Member : BaseEntity
    {
        public Guid ChurchId { get; set; }

        public string MembershipNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string OtherName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string MaritalStatus { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; }

        public string BaptismStatus { get; set; } = string.Empty;

        public string EmergencyContactName { get; set; } = string.Empty;

        public string EmergencyContactPhone { get; set; } = string.Empty;

        public Guid? DepartmentId { get; set; }

        public Guid? FamilyId { get; set; }

        public Church Church { get; set; }

        public ICollection<MemberDepartment> MemberDepartments { get; set; }
                = new List<MemberDepartment>();

        public Household? Household { get; set; }
        [JsonIgnore]
        public ICollection<Attendance> Attendances { get; set; }
            = new List<Attendance>();
    }
}
