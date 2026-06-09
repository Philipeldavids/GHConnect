using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class PermissionService
    : IPermissionService
{
    private readonly ApplicationDbContext
        _context;

    private readonly RoleManager<
        Role> _roleManager;

    public PermissionService(
        ApplicationDbContext context,
        RoleManager<Role>
            roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<List<Permission>>
        GetAllAsync()
    {
        return await _context
            .Permissions
            .Select(x =>
                new Permission
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description =
                        x.Description
                })
            .ToListAsync();
    }

    public async Task<Guid>
        CreateAsync(
            CreatePermissionDto dto)
    {
        var permission =
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description =
                    dto.Description
            };

        await _context
            .Permissions
            .AddAsync(permission);

        await _context
            .SaveChangesAsync();

        return permission.Id;
    }

    public async Task AssignPermissionsAsync(
        AssignPermissionDto dto)
    {
        var role =
            await _roleManager
                .FindByIdAsync(
                    dto.RoleId.ToString());

        if (role == null)
            throw new Exception(
                "Role not found");

        var existing =
            await _context
                .RolePermissions
                .Where(x =>
                    x.RoleId ==
                    dto.RoleId)
                .ToListAsync();

        _context
            .RolePermissions
            .RemoveRange(existing);

        var permissions =
            dto.PermissionIds
               .Select(permissionId =>
                    new RolePermission
                    {
                        RoleId =
                            dto.RoleId,

                        PermissionId =
                           Guid.Parse(permissionId)
                    });

        await _context
            .RolePermissions
            .AddRangeAsync(
                permissions);

        await _context
            .SaveChangesAsync();
    }

    public async Task<RolePermissionDto>
        GetRolePermissionsAsync(
            string roleId)
    {
        var role =
            await _roleManager
                .FindByIdAsync(roleId);

        if (role == null)
            throw new Exception(
                "Role not found");

        var permissions =
            await _context
                .RolePermissions
                .Where(x =>
                    x.RoleId ==
                    roleId)
                .Include(x =>
                    x.Permission)
                .Select(x =>
                    new Permission
                    {
                        Id =
                            x.Permission.Id,

                        Name =
                            x.Permission.Name,

                        Description =
                            x.Permission
                                .Description
                    })
                .ToListAsync();

        return new RolePermissionDto
        {
            RoleId = role.Id,
            RoleName = role.Name!,
            Permissions =
                permissions
        };
    }

    public async Task<List<string>>
        GetPermissionsByRoleAsync(
            string roleId)
    {
        return await _context
            .RolePermissions
            .Where(x =>
                x.RoleId ==
                roleId)
            .Include(x =>
                x.Permission)
            .Select(x =>
                x.Permission.Name)
            .ToListAsync();
    }
}