using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class CommunicationHistoryDto
    {
        public Guid Id { get; set; }

        public string Recipient { get; set; }
            = string.Empty;

        public string Subject { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public string Channel { get; set; }

        public bool IsSuccessful { get; set; }

        public string Status {  get; set; }

        public DateTime SentAt { get; set; }
    }
}
