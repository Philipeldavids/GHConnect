using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class TemplateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
            = string.Empty;

        public string Channel { get; set; }

        public string Subject { get; set; }
            = string.Empty;

        public string Body { get; set; }
            = string.Empty;
    }
}
