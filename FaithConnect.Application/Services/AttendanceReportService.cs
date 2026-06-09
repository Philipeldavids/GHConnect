using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class AttendanceReportService : IAttendanceReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public AttendanceReportService(ApplicationDbContext context, ITenantService tenantService) 
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<List<AttendanceTrendDto>>
    MonthlyTrendAsync()
        {
            var data =
                await _context.Attendances
                    .GroupBy(x =>
                        x.CheckInTime.Month)
                    .Select(x => new
                    {
                        Month = x.Key,
                        Count = x.Count()
                    })
                    .ToListAsync();

            return data
                .Select(x =>
                    new AttendanceTrendDto
                    {
                        Period =
                            CultureInfo
                                .CurrentCulture
                                .DateTimeFormat
                                .GetAbbreviatedMonthName(
                                    x.Month),

                        AttendanceCount =
                            x.Count
                    })
                .OrderBy(x =>
                    DateTime.ParseExact(
                        x.Period,
                        "MMM",
                        CultureInfo.CurrentCulture)
                        .Month)
                .ToList();
        }

        public async Task<List<DepartmentAttendanceDto>>
    DepartmentAttendanceAsync()
        {
            return await _context.MemberDepartments
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
                .ToListAsync();
        }

        public async Task<List<TopAttendeeDto>>
    TopAttendeesAsync()
        {
            var data =
                await _context.Attendances
                    .GroupBy(x => new
                    {
                        x.MemberId,
                        x.Member.FirstName,
                        x.Member.LastName,
                        x.Member.MembershipNumber
                    })
                    .Select(x => new
                    {
                        x.Key.MemberId,
                        x.Key.FirstName,
                        x.Key.LastName,
                        x.Key.MembershipNumber,
                        AttendanceCount =
                            x.Count()
                    })
                    .OrderByDescending(x =>
                        x.AttendanceCount)
                    .Take(10)
                    .ToListAsync();

            return data
                .Select(x =>
                    new TopAttendeeDto
                    {
                        MemberId =
                            x.MemberId,

                        MembershipNumber =
                            x.MembershipNumber,

                        MemberName =
                            $"{x.FirstName} {x.LastName}",

                        AttendanceCount =
                            x.AttendanceCount
                    })
                .ToList();
        }
        public async Task<AttendanceDashboardDto>
    DashboardAsync()
        {
            var churchId =
                _tenantService.GetTenantId();

            var today =
                DateTime.UtcNow.Date;

            var tomorrow =
                today.AddDays(1);

            var totalMembers =
                await _context.Members
                    .CountAsync(x =>
                        x.ChurchId ==
                        churchId);

            var present =
                await _context.Attendances
                    .CountAsync(x =>
                        x.Service.ServiceDate >= today &&
                        x.Service.ServiceDate < tomorrow &&
                        x.Status ==
                            AttendanceStatus.Present);

            var late =
                await _context.Attendances
                    .CountAsync(x =>
                        x.Service.ServiceDate >= today &&
                        x.Service.ServiceDate < tomorrow &&
                        x.Status ==
                            AttendanceStatus.Late);

            var absent =
                Math.Max(
                    0,
                    totalMembers -
                    present -
                    late);

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
                    totalMembers == 0
                        ? 0
                        : Math.Round(
                            ((double)
                                (present + late)
                             / totalMembers)
                            * 100,
                            2)
            };
        }
    }
}