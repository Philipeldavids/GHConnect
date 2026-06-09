using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class AssignMemberDepartmentDto
    {
        public Guid MemberId { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
