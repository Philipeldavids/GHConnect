using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class AssignPermissionDto
    {
        public string RoleId { get; set; }

        public List<string> PermissionIds { get; set; } = [];
    }
}
