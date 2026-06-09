using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class BulkEmailDto
    {
        public string CampaignName { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
        public List<Guid> MemberIds { get; set; }
        = new();
        public List<string> Emails { get; set; }
            = new();
    }
}
