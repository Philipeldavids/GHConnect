using FaithConnect.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IDashboardService
    {
       
            Task<DashboardSummaryDto> GetSummaryAsync();
     }
    
}
