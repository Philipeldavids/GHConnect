using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class MemberPortalService
     : IMemberPortalService
    {
        private readonly ApplicationDbContext
            _context;

        private readonly ITenantService
            _tenantService;

        public MemberPortalService(
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _context = context;
            _tenantService =
                tenantService;
        }

        public async Task<MemberDashboardDto>
            DashboardAsync(
                Guid memberId)
        {
            var member =
                await _context.Members
                    .FirstOrDefaultAsync(
                        x => x.Id ==
                        memberId);

            if (member == null)
                throw new Exception(
                    "Member not found");

            var attended =
                await _context.Attendances
                    .CountAsync(x =>
                        x.MemberId ==
                        memberId);

            var totalServices =
                await _context.Services
                    .CountAsync(x =>
                        x.ChurchId ==
                        member.ChurchId);

            var upcoming =
                await _context.Services
                    .CountAsync(x =>
                        x.ChurchId ==
                            member.ChurchId &&
                        x.ServiceDate >=
                            DateTime.UtcNow);

            return new MemberDashboardDto
            {
                MemberName =
                    $"{member.FirstName} {member.LastName}",

                ServicesAttended =
                    attended,

                UpcomingServices =
                    upcoming,

                AttendancePercentage =
                    totalServices == 0
                        ? 0
                        : Math.Round(
                            ((double)
                                attended /
                                totalServices)
                            * 100,
                            2)
            };
        }

        public async Task<Member>
            ProfileAsync(
                Guid memberId)
        {
            return await _context.Members
                .Where(x =>
                    x.Id ==
                    memberId)
                .Select(x =>
                    new Member
                    {
                        Id = x.Id,
                        MembershipNumber =
                            x.MembershipNumber,
                        FirstName =
                            x.FirstName,
                        LastName =
                            x.LastName,
                        Email =
                            x.Email,
                        PhoneNumber =
                            x.PhoneNumber,
                        Address =
                            x.Address,
                        Occupation =
                            x.Occupation,
                        EmergencyContactName =
    x.EmergencyContactName,

                        EmergencyContactPhone =
    x.EmergencyContactPhone,
                    })
                .FirstAsync();
        }

        public async Task UpdateProfileAsync(
            Guid memberId,
            UpdateMemberProfileDto dto)
        {
            var member =
                await _context.Members
                    .FirstOrDefaultAsync(
                        x => x.Id ==
                        memberId);

            if (member == null)
                throw new Exception(
                    "Member not found");

            member.Email =
                dto.Email;

            member.PhoneNumber =
                dto.PhoneNumber;

            member.Address =
                dto.Address;

            member.Occupation =
                dto.Occupation;

            member.EmergencyContactName =
                dto.EmergencyContactName;

            member.EmergencyContactPhone =
                dto.EmergencyContactPhone;

            await _context
                .SaveChangesAsync();
        }

        public async Task<List<MemberAttendanceHistoryDto>>
            AttendanceAsync(
                Guid memberId)
        {
            return await _context
                .Attendances
                .Where(x =>
                    x.MemberId ==
                    memberId)
                .Select(x =>
                    new MemberAttendanceHistoryDto
                    {
                        AttendanceId =
        x.Id,

                        ServiceName =
        x.Service.Name,

                        ServiceDate =
        x.Service.ServiceDate,

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
                .OrderByDescending(x =>
                    x.ServiceDate)
                .ToListAsync();
        }

        public async Task<List<ServiceDto>>
            UpcomingServicesAsync(
                Guid churchId)
        {
            return await _context
                .Services
                .Where(x =>
                    x.ChurchId ==
                        churchId &&
                    x.ServiceDate >=
                        DateTime.UtcNow)
                .OrderBy(x =>
                    x.ServiceDate)
                .Select(x =>
                    new ServiceDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ServiceDate =
                            x.ServiceDate
                    })
                .ToListAsync();
        }
    }
}
