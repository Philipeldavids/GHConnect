using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IUserService
    {

        Task<List<User>> GetAllAsync();

        Task<UserDetailDto> GetByIdAsync(string id);

        Task<string> CreateAsync(
            CreateUserDto dto);

        Task UpdateAsync(
            string id,
            UpdateUserDto dto);

        Task ChangePasswordAsync(
    string userId,
    ChangePasswordDto dto);

        Task<string>
ResetPasswordAsync(
    string userId);

        Task DeleteAsync(
            string id);

        //Task AssignRolesAsync(
        //    AssignRoleDto dto);
        Task<List<string>> GetUserRolesAsync(
    string userId);

        Task AssignRolesAsync(
            AssignUserRolesDto dto);

        Task RemoveRoleAsync(
            string userId,
            string roleName);
    }
}
