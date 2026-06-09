using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class CreateTemplateDto
    {
        public string Name { get; set; }
            = string.Empty;

        public string Channel { get; set; }

        public string Subject { get; set; }
            = string.Empty;

        public string Content { get; set; }
            = string.Empty;
    }
}
