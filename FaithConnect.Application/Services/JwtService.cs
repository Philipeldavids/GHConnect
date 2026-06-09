using FaithConnect.Application.Interfaces;
using FaithConnect.Application.Utilities;
using FaithConnect.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        public JwtService(
            IConfiguration configuration, ITokenGenerator tokenGenerator,
            UserManager<User> userManager)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessToken(
            User user)
        {
            return await _tokenGenerator.GenerateToken(user);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64)
            );
        }
    }
}
