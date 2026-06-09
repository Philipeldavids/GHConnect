using FaithConnect.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(
            LoginDto dto);

        Task<AuthResponseDto> RegisterAsync(
    RegisterDto dto);

        Task<LoginResponseDto> RefreshAsync(
            string refreshToken);

        Task LogoutAsync(
            string refreshToken);
    }
}
