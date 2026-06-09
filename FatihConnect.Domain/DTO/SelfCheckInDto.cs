using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Domain.DTO
{
    public class SelfCheckInDto
    {
        public Guid ServiceId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
