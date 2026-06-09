using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(User user);

        string GenerateRefreshToken();
    }
}
