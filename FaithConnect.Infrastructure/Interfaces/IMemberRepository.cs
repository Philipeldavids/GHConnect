using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Interfaces
{
    public interface IMemberRepository
    {

        Task<bool> EmailExistsAsync(
    string email,
    Guid churchId);

        Task<bool> PhoneExistsAsync(
            string phoneNumber,
            Guid churchId);
        Task<List<Member>> GetAllAsync(Guid churchId);

        Task<Member?> GetByIdAsync(Guid id, Guid churchId);

        Task<Member?> GetByEmailAsync(
            string email,
            Guid churchId);

        Task CreateAsync(Member member);

        Task UpdateAsync(Member member);

        Task DeleteAsync(Member member);

        Task<Member?> GetMemberDetailsAsync(Guid churchId, Guid id);

        Task<List<Attendance>> GetAttendanceHistoryAsync(Guid memberId);

        Task<List<CommunicationLog>> GetCommunicationHistoryAsync(Guid memberId);
    }
}
