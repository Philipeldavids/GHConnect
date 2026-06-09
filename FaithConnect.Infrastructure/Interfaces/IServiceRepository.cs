using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);

        Task<List<Service>> GetAllAsync(Guid churchId);

        Task<Service?> GetActiveServiceAsync(Guid churchId);

        Task CreateAsync(Service service);

        Task UpdateAsync(Service service);

        Task DeleteAsync(Guid id);
    }
}
