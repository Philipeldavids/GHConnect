using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllAsync();

        Task<ServiceDto> GetAsync(Guid id);

        Task CreateAsync(
            CreateServiceDto dto);

        Task UpdateAsync(
            Guid id,
            UpdateServiceDto dto);

        Task DeleteAsync(Guid id);

        Task<List<ServiceDto>>
            UpcomingServicesAsync();
    }
}
