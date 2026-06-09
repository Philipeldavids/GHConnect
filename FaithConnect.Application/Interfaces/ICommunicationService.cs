using FaithConnect.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface ICommunicationService
    {
        Task SendBulkSmsAsync(
            BulkSmsDto dto);

        Task SendBulkEmailAsync(
            BulkEmailDto dto);

        Task<List<CommunicationHistoryDto>>
            GetHistoryAsync();

        Task<List<CommunicationHistoryDto>>
            GetMemberHistoryAsync(
                Guid memberId);
    }
}
