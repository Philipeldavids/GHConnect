using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Repository
{
    public class AttendanceRepository
    : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            Attendance attendance)
        {
            await _context.Attendances
                .AddAsync(attendance);

            await _context.SaveChangesAsync();
        }
        public async Task<List<Attendance>> GetTodayAttendanceAsync(Guid OrgId)
        {
            return await _context.Attendances
                .Where(x=>x.CreatedAt <= DateTime.UtcNow.AddDays(1) && x.ChurchId == OrgId )
                .Include(x => x.Member)
                .ToListAsync();
        }
        public async Task<bool> ExistsAsync(
            Guid memberId,
            Guid serviceId)
        {
            return await _context.Attendances
                .AnyAsync(x =>
                    x.MemberId == memberId &&
                    x.ServiceId == serviceId);
        }

        public async Task<List<AttendanceListDto>>
            GetAttendanceByServiceAsync(Guid serviceId)
        {

            return await _context.Attendances
    .Include(x => x.Member)
    .Where(x => x.ServiceId == serviceId)
    .Select(x =>
        new AttendanceListDto
        {
            Id = x.Id,

            MembershipNumber =
                x.Member.MembershipNumber,

            MemberName =
                x.Member.FirstName +
                " " +
                x.Member.LastName,

            Status =
                x.Status.ToString(),

            CheckInTime =
                x.CheckInTime,

            Latitude =
                x.Latitude,

            Longitude =
                x.Longitude,

            DistanceMeters =
                x.DistanceMeters,

            IsWithinGeofence =
                x.IsWithinGeofence
        })
    .ToListAsync();

        }

        public async Task<List<Attendance>>
            GetAttendanceByMemberAsync(Guid memberId)
        {
            return await _context.Attendances
                .Include(x => x.Service)
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.CheckInTime)
                .ToListAsync();
        }
    }
}
