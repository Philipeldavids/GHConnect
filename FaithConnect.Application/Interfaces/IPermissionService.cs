using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<List<Permission>>
            GetAllAsync();

        Task<Guid>
            CreateAsync(
                CreatePermissionDto dto);

        Task AssignPermissionsAsync(
            AssignPermissionDto dto);

        Task<RolePermissionDto>
            GetRolePermissionsAsync(
                string roleId);

        Task<List<string>>
            GetPermissionsByRoleAsync(
                string roleId);
    }
}
