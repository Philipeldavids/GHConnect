using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class BulkMemberUploadResponseDto
    {
        public int TotalRows { get; set; }

        public int Successful { get; set; }

        public int Failed { get; set; }

        public List<BulkUploadErrorDto> Errors { get; set; }
            = new();
    }
}
