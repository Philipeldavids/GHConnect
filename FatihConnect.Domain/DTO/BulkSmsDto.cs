using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class BulkSmsDto
    {
        public string CampaignName { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
        public List<Guid> MemberIds { get; set; }
        = new();

        public List<string> PhoneNumbers { get; set; }
            = new();
    }
}
