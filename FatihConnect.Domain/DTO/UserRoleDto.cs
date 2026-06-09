using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class UserRoleDto
    {
        public string UserId { get; set; }
            = string.Empty;

        public string UserName { get; set; }
            = string.Empty;

        public List<string> Roles { get; set; }
            = new();
    }
}
