using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IMemberService
    {

        Task<MemberDetailsDto> GetDetailsAsync(Guid id);

        Task<List<MemberAttendanceDto>> GetAttendanceHistoryAsync(Guid memberId);

        Task<List<MemberCommunicationDto>> GetCommunicationHistoryAsync(Guid memberId);
        Task<BulkMemberUploadResponseDto>
    BulkUploadAsync(IFormFile file);
        Task<List<MemberResponseDto>> GetAllAsync();

        Task<MemberResponseDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateMemberDto dto);

        Task UpdateAsync(
            Guid id,
            UpdateMemberDto dto);

        Task DeleteAsync(Guid id);
    }
}
