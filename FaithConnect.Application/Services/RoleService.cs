using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FaithConnect.Application.Services
{ 

    public class RoleService : IRoleService
    {
        private readonly RoleManager<
           Role> _roleManager;

        public RoleService(
            RoleManager<Role>
                roleManager)
        {
            _roleManager =
                roleManager;
        }

        public async Task<List<Role>>
            GetAllAsync()
        {
            return await _roleManager
                .Roles
                .Select(x =>
                    new Role
                    {
                        Id = x.Id,
                        Name = x.Name!
                    })
                .ToListAsync();
        }

        public async Task<RoleDetailDto>
            GetByIdAsync(string id)
        {
            var role =
                await _roleManager
                    .FindByIdAsync(id);

            if (role == null)
                throw new Exception(
                    "Role not found");

            return new RoleDetailDto
            {
                Id = role.Id,
                Name = role.Name!
            };
        }

        public async Task<string>
            CreateAsync(
                CreateRoleDto dto)
        {
            var role =
                new Role
                {
                    Name = dto.Name,

                };

            var result =
                await _roleManager
                    .CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ",",
                        result.Errors
                            .Select(x =>
                                x.Description)));
            }

            return role.Id;
        }

        public async Task UpdateAsync(
            string id,
            UpdateRoleDto dto)
        {
            var role =
                await _roleManager
                    .FindByIdAsync(id);

            if (role == null)
                throw new Exception(
                    "Role not found");

            role.Name =
                dto.Name;

            await _roleManager
                .UpdateAsync(role);
        }

        public async Task DeleteAsync(
            string id)
        {
            var role =
                await _roleManager
                    .FindByIdAsync(id);

            if (role == null)
                throw new Exception(
                    "Role not found");

            await _roleManager
                .DeleteAsync(role);
        }
    }
}
