using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class RolePermissionDto
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public List<Permission>
            Permissions
        { get; set; }
            = [];
    }
}
