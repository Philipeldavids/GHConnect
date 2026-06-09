using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Utilities
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;


        public TokenGenerator(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(User user)
        {

            var roles = await 
     _userManager
        .GetRolesAsync(user);

            Debug.WriteLine($"User Id: {user.Id}");
            Debug.WriteLine($"Email: {user.Email}");
            Debug.WriteLine($"MemberId: {user.MemberId}");
            Debug.WriteLine($"ChurchId: {user.ChurchId}");
            var authClaims =
    new List<Claim>
    {
        new Claim(
            JwtRegisteredClaimNames.Sub,
            user.Id),

        new Claim(
            JwtRegisteredClaimNames.Email,
            user.Email ?? ""),

        new Claim(
            ClaimTypes.Name,
            user.UserName ?? ""),

        new Claim(
            "ChurchId",
            user.ChurchId.ToString()),

        new Claim(
            "MemberId",
            user.MemberId.ToString()
               )
    };

            foreach (var role in roles)
            {
                authClaims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role
                    ));
            }
            var jwt = _configuration.GetSection("Jwt");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: authClaims,
                        expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["AccessTokenExpiryMinutes"])),
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;


        }
    }
}
