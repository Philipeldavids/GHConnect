using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IMemberPortalService
    {
        Task<MemberDashboardDto>
            DashboardAsync(
                Guid memberId);

        Task<Member>
            ProfileAsync(
                Guid memberId);

        Task UpdateProfileAsync(
            Guid memberId,
            UpdateMemberProfileDto dto);

        Task<List<MemberAttendanceHistoryDto>>
            AttendanceAsync(
                Guid memberId);

        Task<List<ServiceDto>>
            UpcomingServicesAsync(
                Guid churchId);
    }
}
