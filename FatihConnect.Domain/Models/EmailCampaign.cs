using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.Models
{
    public class EmailCampaign : BaseEntity
{
    public Guid ChurchId { get; set; }

    //public string CampaignName { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    //public int TotalRecipients { get; set; }

    public int TotalSent { get; set; }

    public int TotalFailed { get; set; }

    public DateTime SentAt { get; set; }

    
}
}
