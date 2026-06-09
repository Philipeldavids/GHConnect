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
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public DashboardService(
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var churchId = _tenantService.GetTenantId();

            var today = DateTime.UtcNow.Date;

            var startOfMonth =
                new DateTime(
                    today.Year,
                    today.Month,
                    1);

            // Members

            var totalMembers =
                await _context.Members
                    .CountAsync(x =>
                        x.ChurchId == churchId);

            var newMembersThisMonth =
                await _context.Members
                    .CountAsync(x =>
                        x.ChurchId == churchId &&
                        x.CreatedAt >= startOfMonth);

            var birthdaysThisMonth =
                await _context.Members
                    .CountAsync(x =>
                        x.ChurchId == churchId &&
                        x.DateOfBirth.HasValue &&
                        x.DateOfBirth.Value.Month == today.Month);

            // Attendance

            var presentToday =
                await _context.Attendances
                    .CountAsync(x =>
    x.Status == AttendanceStatus.Present &&
    x.CheckInTime >= today &&
    x.CheckInTime < today.AddDays(1));

            var lateToday =
                await _context.Attendances
                    .CountAsync(x =>
    x.Status == AttendanceStatus.Late &&
    x.CheckInTime >= today &&
    x.CheckInTime < today.AddDays(1));

            var absentToday =
                Math.Max(
                    0,
                    totalMembers -
                    presentToday -
                    lateToday);

            decimal attendanceRate = 0;

            if (totalMembers > 0)
            {
                attendanceRate =
                    Math.Round(
                        ((decimal)
                            (presentToday + lateToday)
                         / totalMembers)
                        * 100,
                        2);
            }

            // Departments

            var departmentCount =
                await _context.Departments
                    .CountAsync(x =>
                        x.ChurchId == churchId);

            // SMS

            var smsSent =
     await _context.CommunicationLogs
         .CountAsync(x =>
             x.ChurchId == churchId &&
             x.Channel == "SMS");

            var emailsSent =
                await _context.CommunicationLogs
                    .CountAsync(x =>
                        x.ChurchId == churchId &&
                        x.Channel == "Email");

            // Trend

            var yearStart =
    new DateTime(today.Year, 1, 1);

            var yearEnd =
                yearStart.AddYears(1);


            var trendData =
    await _context.Attendances
       .Where(x =>
    x.CheckInTime >= yearStart &&
    x.CheckInTime < yearEnd)
        .GroupBy(x =>
            x.CheckInTime.Month)
        .Select(x => new
        {
            Month = x.Key,
            Count = x.Count()
        })
        .ToListAsync();

            var attendanceTrend =
    trendData
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
        .ToList();

            // Top Attendees
            var attendees =
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
            AttendanceCount = x.Count()
        })
        .OrderByDescending(x =>
            x.AttendanceCount)
        .Take(10)
        .ToListAsync();
            var topAttendees =
    attendees
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

            return new DashboardSummaryDto
            {
                TotalMembers =
                    totalMembers,

                PresentToday =
                    presentToday,

                LateToday =
                    lateToday,

                AbsentToday =
                    absentToday,

                AttendanceRate =
                    attendanceRate,

                SmsSent =
                    smsSent,

                EmailsSent =
                    emailsSent,

                DepartmentCount =
                    departmentCount,

                NewMembersThisMonth =
                    newMembersThisMonth,

                BirthdaysThisMonth =
                    birthdaysThisMonth,

                AttendanceTrend =
                    attendanceTrend,

                TopAttendees =
                    topAttendees
            };
        }
    }
}