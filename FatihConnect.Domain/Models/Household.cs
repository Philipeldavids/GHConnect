using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class Household : BaseEntity
    {
        public Guid ChurchId { get; set; }

        public string HouseholdName { get; set; } = string.Empty;

        public ICollection<Member> Members { get; set; }
            = new List<Member>();
    }
}
