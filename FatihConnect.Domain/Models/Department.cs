using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{

        public class Department : BaseEntity
        {
            public Guid ChurchId { get; set; }
            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public ICollection<MemberDepartment> MemberDepartments { get; set; }
                = new List<MemberDepartment>();
        }
    
}
