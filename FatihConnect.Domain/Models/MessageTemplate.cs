using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class MessageTemplate : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Channel { get; set; } = string.Empty;

        // SMS or Email

        public string? Subject { get; set; }

        public string Body { get; set; } = string.Empty;

        public Guid ChurchId { get; set; }
    }
}
