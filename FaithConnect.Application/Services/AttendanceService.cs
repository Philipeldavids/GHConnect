using FaithConnect.Application.Interfaces;
using FaithConnect.Application.Utilities;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ITenantService _tenantService;
        private readonly ApplicationDbContext _context;

        public AttendanceService(
            IAttendanceRepository attendanceRepository, ITenantService tenantService,
            IServiceRepository serviceRepository, ApplicationDbContext context)
        {
            _attendanceRepository = attendanceRepository;
            _serviceRepository = serviceRepository;
            _tenantService = tenantService;
            _context = context;
        }
        public async Task ManualCheckInAsync(
    ManualAttendanceDto dto)
        {
            var exists =
                await _context.Attendances
                    .AnyAsync(x =>
                        x.ServiceId ==
                            dto.ServiceId &&
                        x.MemberId ==
                            dto.MemberId);

            if (exists)
                throw new Exception(
                    "Member already checked in");

            var attendance =
                new Attendance
                {
                    Id = Guid.NewGuid(),
                    ServiceId =
                        dto.ServiceId,
                    MemberId =
                        dto.MemberId,
                    Status =
                        dto.Status,
                    CheckInTime =
                        DateTime.UtcNow
                };

            await _context.Attendances
                .AddAsync(attendance);

            await _context.SaveChangesAsync();
        }
        public async Task SelfCheckInAsync(
            Guid memberId,
            SelfCheckInDto dto)
        {
            var service =
                await _serviceRepository.GetByIdAsync(
                    dto.ServiceId);

            if (service == null)
                throw new Exception("Service not found");

            var exists =
                await _attendanceRepository.ExistsAsync(
                    memberId,
                    dto.ServiceId);

            if (exists)
                throw new Exception(
                    "Attendance already recorded");

            var distance =
                GeoHelper.CalculateDistance(
                    dto.Latitude,
                    dto.Longitude,
                    service.Latitude,
                    service.Longitude);

            var status =
                AttendanceStatus.Present;

            var currentTime =
                DateTime.Now.TimeOfDay;

            if (currentTime > service.LateThreshold)
            {
                status = AttendanceStatus.Late;
            }

            var attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                ChurchId = service.ChurchId,
                ServiceId = dto.ServiceId,
                MemberId = memberId,
                CheckInTime = DateTime.UtcNow,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                DistanceMeters = distance,
                IsWithinGeofence =
                    distance <= service.AllowedRadiusMeters,
                Status = status
            };

            await _attendanceRepository
                .CreateAsync(attendance);
        }

        public async Task<List<AttendanceTrendDto>>
    MonthlyTrendAsync()
        {
            var data =
                await _context.Attendances
                    .Where(x =>
                        x.Service.ChurchId ==
                        _tenantService.GetTenantId())
                    .Select(x => new
                    {
                        Month =
                            x.CheckInTime.Month
                    })
                    .ToListAsync();

            return data
                .GroupBy(x => x.Month)
                .Select(x =>
                    new AttendanceTrendDto
                    {
                        Period =
                            CultureInfo
                                .CurrentCulture
                                .DateTimeFormat
                                .GetAbbreviatedMonthName(
                                    x.Key),

                        AttendanceCount =
                            x.Count()
                    })
                .OrderBy(x => DateTime.ParseExact(
                    x.Period,
                    "MMM",
                    CultureInfo.CurrentCulture)
                    .Month)
                .ToList();
        }

        public async Task<List<DepartmentAttendanceDto>>
    DepartmentAttendanceAsync()
        {
            var churchId =
                _tenantService.GetTenantId();

            var data =
                await _context.MemberDepartments
                    .Include(x =>
                        x.Department)
                    .Where(x =>
                        x.Department.ChurchId ==
                        churchId)
                    .ToListAsync();

            return data
                .GroupBy(x =>
                    x.Department.Name)
                .Select(x =>
                    new DepartmentAttendanceDto
                    {
                        DepartmentName =
                            x.Key,

                        AttendanceCount =
                            x.Count()
                    })
                .OrderByDescending(x =>
                    x.AttendanceCount)
                .ToList();
        }

        public async Task<AttendanceDashboardDto>
    DashboardAsync()
        {
            var churchId =
                _tenantService.GetTenantId();

            var today =
                DateTime.UtcNow.Date;

            var totalMembers =
                await _context.Members
                    .CountAsync(x =>
                        x.ChurchId ==
                        churchId);

            var todayServiceIds =
                await _context.Services
                    .Where(x =>
                        x.ChurchId ==
                            churchId &&
                        x.ServiceDate.Date ==
                            today)
                    .Select(x =>
                        x.Id)
                    .ToListAsync();

            var attendance =
                await _context.Attendances
                    .Where(x =>
                        todayServiceIds
                            .Contains(
                                x.ServiceId))
                    .ToListAsync();

            var present =
                attendance.Count(x =>
                    x.Status ==
                    AttendanceStatus.Present);

            var late =
                attendance.Count(x =>
                    x.Status ==
                    AttendanceStatus.Late);

            var absent =
                Math.Max(
                    0,
                    totalMembers -
                    present -
                    late);

            var attendanceRate =
                totalMembers == 0
                    ? 0
                    : Math.Round(
                        ((double)
                            (present + late)
                            / totalMembers)
                        * 100,
                        2);

            return new AttendanceDashboardDto
            {
                TotalMembers =
                    totalMembers,

                PresentToday =
                    present,

                LateToday =
                    late,

                AbsentToday =
                    absent,

                AttendanceRate =
                    attendanceRate
            };
        }
        public async Task<List<TopAttendeeDto>>
    TopAttendeesAsync()
        {
            var data =
                await _context.Attendances
                    .Include(x =>
                        x.Member)
                    .Where(x =>
                        x.Member.ChurchId ==
                        _tenantService.GetTenantId())
                    .ToListAsync();

            return data
                .GroupBy(x =>
                    x.MemberId)
                .Select(x =>
                {
                    var member =
                        x.First().Member;

                    return new TopAttendeeDto
                    {
                        MemberId =
                            member.Id,

                        MembershipNumber =
                            member.MembershipNumber,

                        MemberName =
                            $"{member.FirstName} {member.LastName}",

                        AttendanceCount =
                            x.Count()
                    };
                })
                .OrderByDescending(x =>
                    x.AttendanceCount)
                .Take(10)
                .ToList();
        }

        public async Task<List<MemberAttendanceHistoryDto>>
    MemberHistoryAsync(
        Guid memberId)
        {
            return await _context.Attendances
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
                            x.CheckInTime
                    })
                .OrderByDescending(x =>
                    x.ServiceDate)
                .ToListAsync();
        }
        public async Task<List<AttendanceListDto>>
            GetServiceAttendanceAsync(Guid serviceId)
        {
            return await _attendanceRepository
                .GetAttendanceByServiceAsync(serviceId);

           
        }
        public async Task<List<Attendance>> GetTodayAttendanceAsync()
        {
            var orgId = _tenantService.GetTenantId();

            var attendance = await _attendanceRepository.GetTodayAttendanceAsync(orgId);

            return attendance;
            
        
        }

        public async Task<ServiceAttendanceSummaryDto>
    ServiceSummaryAsync(
        Guid serviceId)
        {
            var service =
                await _context.Services
                    .FirstOrDefaultAsync(x =>
                        x.Id ==
                        serviceId);

            if (service == null)
                throw new Exception(
                    "Service not found");

            var attendance =
                await _context.Attendances
                    .Where(x =>
                        x.ServiceId ==
                        serviceId)
                    .ToListAsync();

            return new ServiceAttendanceSummaryDto
            {
                ServiceId =
                    service.Id,

                ServiceName =
                    service.Name,

                Present =
                    attendance.Count(x =>
                        x.Status ==
                        AttendanceStatus.Present),

                Late =
                    attendance.Count(x =>
                        x.Status ==
                        AttendanceStatus.Late),

                Absent =
                    attendance.Count(x =>
                        x.Status ==
                        AttendanceStatus.Absent),

                TotalAttendance =
                    attendance.Count
            };
        }
        public async Task<List<Attendance>>
            GetMemberAttendanceAsync(Guid memberId)
        {
            return await _attendanceRepository
                .GetAttendanceByMemberAsync(memberId);
        }
    }
}
