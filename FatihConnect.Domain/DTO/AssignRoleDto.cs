using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class AssignRoleDto
    {
        public string UserId { get; set; }

        public List<string> RoleIds { get; set; }
    }
}
