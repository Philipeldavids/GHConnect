using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllAsync();

        Task<RoleDetailDto> GetByIdAsync(
            string id);

        Task<string> CreateAsync(
            CreateRoleDto dto);

        Task UpdateAsync(
            string id,
            UpdateRoleDto dto);

        Task DeleteAsync(
            string id);
    }
}
