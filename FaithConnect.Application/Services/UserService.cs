using FaithConnect.Application.Interfaces;
using FaithConnect.Application.Utilities;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ITenantService _tenantService;
    private readonly IEmailService _emailService;
    public UserService(
        UserManager<User> userManager, IEmailService emailService, ITenantService tenantService)
    {
        _userManager = userManager;
        _tenantService = tenantService;
        _emailService = emailService;
    }
    public async Task<List<string>>
    GetUserRolesAsync(
        string userId)
    {
        var user =
            await _userManager
                .FindByIdAsync(userId);

        if (user == null)
        {
            throw new Exception(
                "User not found");
        }

        return (
            await _userManager
                .GetRolesAsync(user)
        ).ToList();
    }

    public async Task AssignRolesAsync(
    AssignUserRolesDto dto)
    {
        var user =
            await _userManager
                .FindByIdAsync(dto.UserId);

        if (user == null)
        {
            throw new Exception(
                "User not found");
        }

        var existingRoles =
            await _userManager
                .GetRolesAsync(user);

        await _userManager
            .RemoveFromRolesAsync(
                user,
                existingRoles);

        if (dto.Roles.Any())
        {
            await _userManager
                .AddToRolesAsync(
                    user,
                    dto.Roles);
        }
    }
    public async Task<string>
ResetPasswordAsync(
    string userId)
    {
        var user =
            await _userManager
                .FindByIdAsync(userId);

        if (user == null)
            throw new Exception(
                "User not found");

        var password =
            PasswordGenerator
                .Generate(10);

        var token =
            await _userManager
                .GeneratePasswordResetTokenAsync(
                    user);

        var result =
            await _userManager
                .ResetPasswordAsync(
                    user,
                    token,
                    password);


        if (!result.Succeeded)
            throw new Exception(
                "Password reset failed");

                user.MustChangePassword = true;
        await _userManager.UpdateAsync(user);


        await _emailService
            .SendEmailAsync(
                user.Email!,
                "Password Reset",
                $"Your new password is: {password}");

        return password;
    }
    public async Task RemoveRoleAsync(
    string userId,
    string roleName)
    {
        var user =
            await _userManager
                .FindByIdAsync(userId);

        if (user == null)
        {
            throw new Exception(
                "User not found");
        }

        await _userManager
            .RemoveFromRoleAsync(
                user,
                roleName);
    }

    public async Task<List<User>>
        GetAllAsync()
    {

        var orgId = _tenantService.GetTenantId();
        var users =
            await _userManager.Users.Where(x=>x.ChurchId == orgId)
                .ToListAsync();

        var result =
            new List<User>();

        foreach (var user in users)
        {
            var roles =
                await _userManager
                    .GetRolesAsync(user);

            result.Add(
                new User
                {
                    Id = user.Id,
                    FullName =
                        user.FullName,
                    Email =
                        user.Email,
                    PhoneNumber =
                        user.PhoneNumber,
                    IsActive =
                        user.IsActive,
                    Roles =
                        roles.ToList()
                });
        }

        return result;
    }

    public async Task<UserDetailDto>
        GetByIdAsync(string id)
    {
        var user =
            await _userManager
                .FindByIdAsync(id);

        if (user == null)
            throw new Exception(
                "User not found");

        var roles =
            await _userManager
                .GetRolesAsync(user);

        return new UserDetailDto
        {
            Id = user.Id,
            FullName =
                user.FullName,
            Email =
                user.Email,
            PhoneNumber =
                user.PhoneNumber,
            IsActive =
                user.IsActive,
            Roles =
                roles.Select(x =>
                    new Role
                    {
                        Name = x
                    })
                .ToList()
        };
    }

    public async Task<string>
        CreateAsync(
            CreateUserDto dto)
    {
        var orgId = _tenantService.GetTenantId();
        var user = new User
        {
            Id =
                Guid.NewGuid()
                    .ToString(),

            FullName =
                dto.FullName,
            ChurchId = orgId,
            Email =
                dto.Email,

            UserName =
                dto.Email,

            PhoneNumber =
                dto.PhoneNumber,

            IsActive = true
        };

        var result =
            await _userManager
                .CreateAsync(
                    user,
                    dto.Password);

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(
                    ", ",
                    result.Errors
                        .Select(x =>
                            x.Description)));
        }

        if (dto.RoleIds.Any())
        {
            await _userManager
                .AddToRolesAsync(
                    user,
                    dto.RoleIds);
        }

        return user.Id;
    }

    public async Task ChangePasswordAsync(
    string userId,
    ChangePasswordDto dto)
    {
        if (dto.NewPassword != dto.ConfirmPassword)
            throw new Exception(
                "Passwords do not match");

        var user =
            await _userManager
                .FindByIdAsync(userId);

        if (user == null)
            throw new Exception(
                "User not found");

        var result =
            await _userManager
                .ChangePasswordAsync(
                    user,
                    dto.CurrentPassword,
                    dto.NewPassword);

        if (!result.Succeeded)
            throw new Exception(
                string.Join(
                    ",",
                    result.Errors
                        .Select(x =>
                            x.Description)));
        user.MustChangePassword = false;
        await _userManager.UpdateAsync(user);
    }
    public async Task UpdateAsync(
        string id,
        UpdateUserDto dto)
    {
        var orgId = _tenantService.GetTenantId();
        var user =
            await _userManager
                .FindByIdAsync(id);

        if (user == null)
            throw new Exception(
                "User not found");

        user.FullName =
            dto.FullName;

        user.ChurchId = orgId;
        user.Email =
            dto.Email;

        user.UserName =
            dto.Email;
        user.PhoneNumber =
            dto.PhoneNumber;

        user.IsActive =
            dto.IsActive;

        await _userManager
            .UpdateAsync(user);
    }

    public async Task DeleteAsync(
        string id)
    {
        var user =
            await _userManager
                .FindByIdAsync(id);

        if (user == null)
            throw new Exception(
                "User not found");

        await _userManager
            .DeleteAsync(user);
    }

    public async Task AssignRolesAsync(
        AssignRoleDto dto)
    {
        var user =
            await _userManager
                .FindByIdAsync(
                    dto.UserId);

        if (user == null)
            throw new Exception(
                "User not found");

        var currentRoles =
            await _userManager
                .GetRolesAsync(user);

        await _userManager
            .RemoveFromRolesAsync(
                user,
                currentRoles);

        await _userManager
            .AddToRolesAsync(
                user,
                dto.RoleIds);
    }
}