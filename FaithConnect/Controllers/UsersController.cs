using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController
     : ControllerBase
    {
        private readonly IUserService
            _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(
            IUserService userService, UserManager<User> userManager)
        {
            _userService =
                userService;
            _userManager = userManager;
        }

        [HttpDelete(
    "{userId}/roles/{roleName}")]
        public async Task<IActionResult>
    RemoveRole(
        string userId,
        string roleName)
        {
            await _userService
                .RemoveRoleAsync(
                    userId,
                    roleName);

            return Ok();
        }
        [HttpPost("assign-roles")]
        public async Task<IActionResult>
    AssignRoles(
        AssignUserRolesDto dto)
        {
            await _userService
                .AssignRolesAsync(dto);

            return Ok();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult>
    GetRoles(string id)
        {
            return Ok(
                await _userService
                    .GetUserRolesAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            return Ok(
                await _userService
                    .GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(
                string id)
        {
            return Ok(
                await _userService
                    .GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreateUserDto dto)
        {
            return Ok(
                await _userService
                    .CreateAsync(dto));
        }
//        [Authorize(Roles = "SuperAdmin, ChurchAdmin")]
//        [HttpPost("reset-password")]
//        public async Task<IActionResult> ResetPassword(
//[FromBody] AdminResetPasswordDto dto)
//        {
//            var user =
//            await _userManager
//                .FindByIdAsync(
//                    dto.UserId);

//            if (user == null)
//                return NotFound();

//            await _userManager.ChangePasswordAsync(user, dto.NewPassword)

//            await _context.SaveChangesAsync();

//            return Ok();
//        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(
                string id,
                UpdateUserDto dto)
        {
            await _userService
                .UpdateAsync(
                    id,
                    dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(
                string id)
        {
            await _userService
                .DeleteAsync(id);

            return NoContent();
        }

       
    }
}
