using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class UserRole
    {
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public string RoleId { get; set; }

        public Role Role { get; set; }
    }
}
