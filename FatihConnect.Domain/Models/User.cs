using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class User : IdentityUser
        {
        public Guid ChurchId { get; set; }

        public Guid MemberId { get; set; }

        public string FullName { get; set; } = string.Empty;

              

        public bool MustChangePassword { get; set; }

        public Member? Member { get; set; }

        
        public List<string>? Roles { get; set; }
        public bool IsActive { get; set; }
        public Church Church { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();
    }
}
