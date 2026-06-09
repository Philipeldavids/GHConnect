using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class MemberCommunicationDto
    {
        public Guid Id { get; set; }

        public string Channel { get; set; } = string.Empty;

        public string? Subject { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }
    }
}
