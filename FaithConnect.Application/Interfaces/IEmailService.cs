using FaithConnect.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string mail, string mesage, string body);
    }
}
