using FaithConnect.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface ITemplateService
    {
        Task<List<TemplateDto>> GetAllAsync();

        Task<TemplateDto> GetByIdAsync(Guid id);

        Task CreateAsync(CreateTemplateDto dto);

        Task UpdateAsync(
            Guid id,
            UpdateTemplateDto dto);

        Task DeleteAsync(Guid id);
    }
}
