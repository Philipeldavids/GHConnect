using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public readonly ITenantService _tenantService;
        public readonly ApplicationDbContext _context;

        public EmailService(IOptions<EmailSettings> settings, ApplicationDbContext context, ITenantService tenantService)
        {
            _settings = settings.Value;
            _context = context;
            _tenantService = tenantService;
        }

        public async Task SendEmailAsync(string mail, string mesage, string body)
        {
            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(
                    _settings.DisplayName,
                    _settings.From
                )
            );
            var campaign = new EmailCampaign
            {
                Id = Guid.NewGuid(),
                ChurchId = _tenantService.GetTenantId(),
                Subject = mail,
                //TotalRecipients = dto.Emails.Count(),
                //CampaignName = dto.CampaignName,
                Body = body,
                SentAt = DateTime.UtcNow
            };

            await _context.EmailCampaigns.AddAsync(campaign);
            
                email.To.Add(
               MailboxAddress.Parse(mail)
           );
                email.Subject = mesage;

                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(
                    _settings.Host,
                    _settings.Port,
                    SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(
                    _settings.Username,
                    _settings.Password
                );

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                


            await _context.SaveChangesAsync();
            
        }
    }

}
