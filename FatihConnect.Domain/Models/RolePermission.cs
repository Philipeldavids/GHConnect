using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class RolePermission : BaseEntity
    {
        [Required]
        public string RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public Guid PermissionId { get; set; }

        public Permission Permission { get; set; } = null!;
    }
}
