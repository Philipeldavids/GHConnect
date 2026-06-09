using DocumentFormat.OpenXml.Spreadsheet;
using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace FaithConnect.Application.Services
{
 
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ApplicationDbContext _context;
        private readonly IMemberRepository _repository;

        public AuthService(
            UserManager<User> userManager,
            IJwtService jwtService,
            IMemberRepository repository,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _context = context;
            _repository = repository;
        }

        public async Task<LoginResponseDto> LoginAsync(
            LoginDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(
                    dto.Email
                );

            if (user == null)
            {
                throw new Exception(
                    "Invalid email or password"
                );
            }

            if (!user.IsActive)
            {
                throw new Exception(
                    "Account is disabled"
                );
            }

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password
                );

            if (!validPassword)
            {
                throw new Exception(
                    "Invalid email or password"
                );
            }

            var roles =
                await _userManager.GetRolesAsync(
                    user
                );
    //        Debug.WriteLine(
    //$"UserId: {user.Id}");

    //        Debug.WriteLine(
    //            $"MemberId: {user.MemberId}");
            var accessToken =
                await _jwtService
                    .GenerateAccessToken(
                        user
                    );

            var refreshToken =
                _jwtService.GenerateRefreshToken();

            await _context.RefreshTokens.AddAsync(
                new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Token = refreshToken,
                    ExpiresAt =
                        DateTime.UtcNow.AddDays(7)
                });

            await _context.SaveChangesAsync();

            return new LoginResponseDto
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken,

                FullName =
                    $"{user.FullName}",

                Email = user.Email!,

                Roles = roles.ToList()
            };
        }

        public async Task<LoginResponseDto> RefreshAsync(
            string refreshToken)
        {
            var token =
                await _context.RefreshTokens
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(
                        x =>
                            x.Token == refreshToken &&
                            !x.IsRevoked &&
                            x.ExpiresAt > DateTime.UtcNow
                    );

            if (token == null)
            {
                throw new Exception(
                    "Invalid refresh token"
                );
            }

            var accessToken =
                await _jwtService
                    .GenerateAccessToken(
                        token.User
                    );

                        

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                FullName =
                    $"{token.User.FullName}",
                Email = token.User.Email!,
                Roles =
                    (await _userManager
                        .GetRolesAsync(token.User))
                    .ToList()
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(
    RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception(
                    "Passwords do not match");
            }

            var existingUser =
                await _userManager.FindByEmailAsync(
                    dto.Email);

            if (existingUser != null)
            {
                throw new Exception(
                    "Email already exists");
            }

            var church = new Church
            {
                Id = Guid.NewGuid(),
                Name = dto.ChurchName,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Churches.AddAsync(
                church);
            

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FullName = dto.FirstName +" "+dto.LastName,                
                Email = dto.Email,              
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                ChurchId = church.Id,
                IsActive = true
            };
            var member = new Domain.Models.Member
            {
                Id = Guid.NewGuid(),
                ChurchId = church.Id,
                MembershipNumber =
                        $"MEM-{DateTime.UtcNow.Ticks}",
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                MembershipDate = DateTime.UtcNow
            };

            user.MemberId = member.Id;

            await _repository.CreateAsync(member);
            var result =
                await _userManager.CreateAsync(
                    user,
                    dto.Password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ",",
                        result.Errors.Select(
                            x => x.Description)));
            }

            await _userManager.AddToRoleAsync(
                user,
                "ChurchAdmin");

            var accessToken =
                await _jwtService
                    .GenerateAccessToken(
                        user);

            var refreshToken =
                _jwtService.GenerateRefreshToken();

            await _context.RefreshTokens.AddAsync(
                new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Token = refreshToken,
                    ExpiresAt =
                        DateTime.UtcNow.AddDays(7)
                });

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                FullName =
                    $"{user.FullName}",
                Email = user.Email!,
                Roles = new()
        {
            "ChurchAdmin"
        }
            };
        }
        public async Task LogoutAsync(
            string refreshToken)
        {
            var token =
                await _context.RefreshTokens
                    .FirstOrDefaultAsync(
                        x => x.Token == refreshToken
                    );

            if (token == null)
                return;

            token.IsRevoked = true;

            await _context.SaveChangesAsync();
        }
    }
}
