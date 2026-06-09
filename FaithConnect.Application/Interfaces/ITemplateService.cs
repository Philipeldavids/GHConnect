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

        Task<TemplateDto> GetAsync(Guid id);

        Task CreateAsync(CreateTemplateDto dto);

        Task UpdateAsync(
            Guid id,
            CreateTemplateDto dto);

        Task DeleteAsync(Guid id);
    }
}
