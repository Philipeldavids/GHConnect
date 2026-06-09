using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class CommunicationLog : BaseEntity
    {
        public Guid ChurchId { get; set; }

        public Guid? MemberId { get; set; }

        public string Channel { get; set; } = string.Empty;

        public string Recipient { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public string? Subject { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public string? ProviderMessageId { get; set; }
        public Member? Member { get; set; }
    }
}
