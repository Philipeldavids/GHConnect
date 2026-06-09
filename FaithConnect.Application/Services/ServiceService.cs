using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class ServiceService
     : IServiceService
    {
        private readonly
            ApplicationDbContext _context;

        private readonly
            ITenantService _tenantService;

        public ServiceService(
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<List<ServiceDto>>
            GetAllAsync()
        {
            var churchId =
                _tenantService.GetTenantId();

            return await _context.Services
                .Where(x =>
                    x.ChurchId == churchId)
                .Select(x =>
                    new ServiceDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ServiceType =
                            x.ServiceType,
                        ServiceDate =
                            x.ServiceDate,
                        StartTime =
                            x.StartTime,
                        LateThreshold =
                            x.LateThreshold,
                        AttendanceCloseTime =
                            x.AttendanceCloseTime,
                        AttendanceEnabled =
                            x.AttendanceEnabled,
                        AllowedRadiusMeters =
                            x.AllowedRadiusMeters,
                        AttendanceCount =
                            x.Attendances.Count()
                    })
                .OrderByDescending(x =>
                    x.ServiceDate)
                .ToListAsync();
        }

        public async Task<ServiceDto>
            GetAsync(Guid id)
        {
            return await _context.Services
                .Where(x =>
                    x.Id == id)
                .Select(x =>
                    new ServiceDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ServiceType =
                            x.ServiceType,
                        ServiceDate =
                            x.ServiceDate,
                        StartTime =
                            x.StartTime,
                        LateThreshold =
                            x.LateThreshold,
                        AttendanceCloseTime =
                            x.AttendanceCloseTime,
                        AttendanceEnabled =
                            x.AttendanceEnabled,
                        AllowedRadiusMeters =
                            x.AllowedRadiusMeters,
                        AttendanceCount =
                            x.Attendances.Count()
                    })
                .FirstAsync();
        }

        public async Task CreateAsync(
            CreateServiceDto dto)
        {
            await _context.Services
                .AddAsync(
                    new Service
                    {
                        ChurchId =
                            _tenantService
                                .GetTenantId(),

                        Name = dto.Name,
                        ServiceType =
                            dto.ServiceType,
                        ServiceDate =
                            dto.ServiceDate,
                        StartTime =
                            TimeSpan.Parse(dto.StartTime),
                        LateThreshold =
                            TimeSpan.Parse(dto.LateThreshold),
                        AttendanceCloseTime =
                            TimeSpan.Parse(dto.AttendanceCloseTime),
                        Latitude =
                            dto.Latitude,
                        Longitude =
                            dto.Longitude,
                        AllowedRadiusMeters =
                            dto.AllowedRadiusMeters,
                        AttendanceEnabled =
                            dto.AttendanceEnabled
                    });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            Guid id,
            UpdateServiceDto dto)
        {
            var service =
                await _context.Services
                    .FindAsync(id);

            if (service == null)
            {
                throw new Exception(
                    "Service not found");
            }

            service.Name =
                dto.Name;

            service.ServiceType =
                dto.ServiceType;

            service.ServiceDate =
                dto.ServiceDate;

            service.StartTime =
                dto.StartTime;

            service.LateThreshold =
                dto.LateThreshold;

            service.AttendanceCloseTime =
                dto.AttendanceCloseTime;

            service.Latitude =
                dto.Latitude;

            service.Longitude =
                dto.Longitude;

            service.AllowedRadiusMeters =
                dto.AllowedRadiusMeters;

            service.AttendanceEnabled =
                dto.AttendanceEnabled;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            Guid id)
        {
            var service =
                await _context.Services
                    .FindAsync(id);

            if (service == null)
            {
                throw new Exception(
                    "Service not found");
            }

            _context.Services
                .Remove(service);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ServiceDto>>
            UpcomingServicesAsync()
        {
            var churchId =
                _tenantService.GetTenantId();

            var today =
                DateTime.UtcNow.Date;

            return await _context.Services
                .Where(x =>
                    x.ChurchId == churchId &&
                    x.ServiceDate >= today)
                .Select(x =>
                    new ServiceDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ServiceType =
                            x.ServiceType,
                        ServiceDate =
                            x.ServiceDate,
                        StartTime =
                            x.StartTime,
                        AttendanceEnabled =
                            x.AttendanceEnabled
                    })
                .OrderBy(x =>
                    x.ServiceDate)
                .ToListAsync();
        }
    }
}
