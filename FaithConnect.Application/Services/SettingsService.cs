using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FaithConnect.Application.Services
{

    public class SettingsService
        : ISettingsService
    {
        private readonly ApplicationDbContext _context;

        private readonly ITenantService _tenantService;

        public SettingsService(
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<ChurchSettingsDto>
            GetAsync()
        {
            var churchId =
                _tenantService.GetTenantId();

            var church =
                await _context.Churches
                    .FirstOrDefaultAsync(x =>
                        x.Id == churchId);

            if (church == null)
                throw new Exception(
                    "Church not found");

            return new ChurchSettingsDto
            {
                Id = church.Id,
                Name = church.Name,
                Email = church.Email,
                PhoneNumber = church.PhoneNumber,
                Address = church.Address,
                Website = church.Website,
                LogoUrl = church.LogoUrl,
                Latitude = church.Latitude,
                Longitude = church.Longitude,
                DefaultAllowedRadiusMeters =
                    church.DefaultAllowedRadiusMeters
            };
        }

        public async Task UpdateAsync(
            UpdateChurchSettingsDto dto)
        {
            var churchId =
                _tenantService.GetTenantId();

            var church =
                await _context.Churches
                    .FirstOrDefaultAsync(x =>
                        x.Id == churchId);

            if (church == null)
                throw new Exception(
                    "Church not found");

            church.Name =
                dto.Name;

            church.Email =
                dto.Email;

            church.PhoneNumber =
                dto.PhoneNumber;

            church.Address =
                dto.Address;

            church.Website =
                dto.Website;

            church.LogoUrl =
                dto.LogoUrl;

            church.Latitude =
                dto.Latitude;

            church.Longitude =
                dto.Longitude;

            church.DefaultAllowedRadiusMeters =
                dto.DefaultAllowedRadiusMeters;

            await _context.SaveChangesAsync();
        }
    }
}
