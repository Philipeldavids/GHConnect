using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class Role :  IdentityRole
    {
        //public string Name { get; set; } = string.Empty;

        public Guid ChurchId {  get; set; }

        public ICollection<User> Users { get; set; }
            = new List<User>();

        public ICollection<UserRole> UserRoles { get; set; }
      = new List<UserRole>();

        public ICollection<RolePermission> RolePermissions { get; set; }
            = new List<RolePermission>();
    }
}
