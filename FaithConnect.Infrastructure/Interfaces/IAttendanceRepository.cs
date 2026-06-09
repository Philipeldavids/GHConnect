using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Interfaces
{
    public interface IAttendanceRepository
    {
        Task CreateAsync(Attendance attendance);
        Task<List<Attendance>> GetTodayAttendanceAsync(Guid OrgId);
        Task<bool> ExistsAsync(
            Guid memberId,
            Guid serviceId);

        Task<List<AttendanceListDto>> GetAttendanceByServiceAsync(
            Guid serviceId);

        Task<List<Attendance>> GetAttendanceByMemberAsync(
            Guid memberId);
    }
}
