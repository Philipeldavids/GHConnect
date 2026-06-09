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
    public class CommunicationService
    : ICommunicationService
    {
        private readonly ApplicationDbContext _context;

        private readonly IEmailService _emailService;

        private readonly ISmsService _smsService;

        private readonly ITenantService _tenantService;

        public CommunicationService(
            ApplicationDbContext context,
            IEmailService emailService,
            ISmsService smsService,
            ITenantService tenantService)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
            _tenantService = tenantService;
        }

        public async Task SendBulkEmailAsync(
    BulkEmailDto dto)
        {
            var tenantId =
                _tenantService.GetTenantId();

            var members =
                await _context.Members
                    .Where(x =>
                        dto.MemberIds.Contains(x.Id))
                    .ToListAsync();

            var campaignId =
                Guid.NewGuid();

            foreach (var member in members)
            {
                try
                {
                    await _emailService
                        .SendEmailAsync(
                            member.Email,
                            dto.Subject,
                            dto.Body);

                    await _context
                        .CommunicationLogs
                        .AddAsync(
                            new CommunicationLog
                            {
                                Id = Guid.NewGuid(),

                                ChurchId =
                                    tenantId,

                                MemberId =
                                    member.Id,

                                ProviderMessageId =
                                    campaignId
                                        .ToString(),

                                Channel =
                                    "Email",

                                Recipient =
                                    member.Email,

                                Subject =
                                    dto.Subject,

                                Message =
                                    dto.Body,

                                Status =
                                    "Sent",

                                SentAt =
                                    DateTime.UtcNow
                            });
                }
                catch (Exception ex)
                {
                    await _context
                        .CommunicationLogs
                        .AddAsync(
                            new CommunicationLog
                            {
                                Id = Guid.NewGuid(),

                                ChurchId =
                                    tenantId,

                                MemberId =
                                    member.Id,

                                ProviderMessageId =
                                    campaignId
                                        .ToString(),

                                Channel =
                                    "Email",

                                Recipient =
                                    member.Email,

                                Subject =
                                    dto.Subject,

                                Message =
                                    dto.Body,

                                Status =
                                    "Failed",

                                ErrorMessage =
                                    ex.Message,

                                SentAt =
                                    DateTime.UtcNow
                            });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task SendBulkSmsAsync(
    BulkSmsDto dto)
        {
            var tenantId =
                _tenantService.GetTenantId();

            var members =
                await _context.Members
                    .Where(x =>
                        dto.MemberIds.Contains(x.Id))
                    .ToListAsync();

            var campaignId =
                Guid.NewGuid();

            foreach (var member in members)
            {
                try
                {
                    await _smsService
                        .SendSmsAsync(
                            member.PhoneNumber,
                            dto.Message);

                    await _context
                        .CommunicationLogs
                        .AddAsync(
                            new CommunicationLog
                            {
                                Id = Guid.NewGuid(),

                                ChurchId =
                                    tenantId,

                                MemberId =
                                    member.Id,

                                ProviderMessageId =
                                    campaignId
                                        .ToString(),

                                Channel =
                                    "SMS",

                                Recipient =
                                    member.PhoneNumber,

                                Message =
                                    dto.Message,

                                Status =
                                    "Sent",

                                SentAt =
                                    DateTime.UtcNow
                            });
                }
                catch (Exception ex)
                {
                    await _context
                        .CommunicationLogs
                        .AddAsync(
                            new CommunicationLog
                            {
                                Id = Guid.NewGuid(),

                                ChurchId =
                                    tenantId,

                                MemberId =
                                    member.Id,

                                ProviderMessageId =
                                    campaignId
                                        .ToString(),

                                Channel =
                                    "SMS",

                                Recipient =
                                    member.PhoneNumber,

                                Message =
                                    dto.Message,

                                Status =
                                    "Failed",

                                ErrorMessage =
                                    ex.Message,

                                SentAt =
                                    DateTime.UtcNow
                            });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<CommunicationHistoryDto>>
    GetHistoryAsync()
        {
            return await _context
                .CommunicationLogs
                .OrderByDescending(x =>
                    x.SentAt)
                .Select(x =>
                    new CommunicationHistoryDto
                    {
                        Id = x.Id,

                        Recipient =
                            x.Recipient,

                        Subject =
                            x.Subject,

                        Message =
                            x.Message,

                        Channel =
                            x.Channel,

                        Status =
                            x.Status,

                        SentAt =
                            x.SentAt
                    })
                .ToListAsync();
        }

        public async Task<List<CommunicationHistoryDto>>
    GetMemberHistoryAsync(
        Guid memberId)
        {
            return await _context
                .CommunicationLogs
                .Where(x =>
                    x.MemberId ==
                    memberId)
                .OrderByDescending(x =>
                    x.SentAt)
                .Select(x =>
                    new CommunicationHistoryDto
                    {
                        Id = x.Id,

                        Recipient =
                            x.Recipient,

                        Subject =
                            x.Subject,

                        Message =
                            x.Message,

                        Channel =
                            x.Channel,

                        Status =
                            x.Status,

                        SentAt =
                            x.SentAt
                    })
                .ToListAsync();
        }
    }

}
