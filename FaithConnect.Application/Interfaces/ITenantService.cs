using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface ITenantService
    {
        Guid GetTenantId();
        void SetTenantId(Guid tenantId);
    }
}
