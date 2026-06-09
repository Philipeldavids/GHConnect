using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class UpdateTemplateDto
    {
        public string Name { get; set; }
            = string.Empty;

        public string Channel { get; set; }
            = string.Empty;

        public string? Subject { get; set; }

        public string Body { get; set; }
            = string.Empty;
    }
}