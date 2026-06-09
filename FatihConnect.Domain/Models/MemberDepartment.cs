using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class MemberDepartment : BaseEntity
    {
        public Guid MemberId { get; set; }

        public Member Member { get; set; } = null!;

        public Guid DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
    }
}
