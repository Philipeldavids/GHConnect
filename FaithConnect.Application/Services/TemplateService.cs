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
    public class TemplateService
    : ITemplateService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public TemplateService(
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _context = context;
            _tenantService = tenantService;
        }

        public async Task<List<TemplateDto>>
            GetAllAsync()
        {
            return await _context.MessageTemplates
                .Where(x =>
                    x.ChurchId ==
                    _tenantService.GetTenantId())
                .Select(x =>
                    new TemplateDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Channel = x.Channel,
                        Subject = x.Subject,
                        Body = x.Body
                    })
                .ToListAsync();
        }

        public async Task<TemplateDto?>
            GetByIdAsync(Guid id)
        {
            return await _context.MessageTemplates
                .Where(x =>
                    x.Id == id)
                .Select(x =>
                    new TemplateDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Channel = x.Channel,
                        Subject = x.Subject,
                        Body = x.Body
                    })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(
            CreateTemplateDto dto)
        {
            var template =
                new MessageTemplate
                {
                    Id = Guid.NewGuid(),
                    ChurchId =
                        _tenantService.GetTenantId(),

                    Name = dto.Name,
                    Channel = dto.Channel,
                    Subject = dto.Subject,
                    Body = dto.Body
                };

            _context.MessageTemplates
                .Add(template);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            Guid id,
            UpdateTemplateDto dto)
        {
            var template =
                await _context.MessageTemplates
                    .FirstOrDefaultAsync(x =>
                        x.Id == id);

            if (template == null)
                throw new Exception(
                    "Template not found");

            template.Name =
                dto.Name;

            template.Channel =
                dto.Channel;

            template.Subject =
                dto.Subject;

            template.Body =
                dto.Body;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            Guid id)
        {
            var template =
                await _context.MessageTemplates
                    .FirstOrDefaultAsync(x =>
                        x.Id == id);

            if (template == null)
                throw new Exception(
                    "Template not found");

            _context.MessageTemplates
                .Remove(template);

            await _context.SaveChangesAsync();
        }
    }
}
