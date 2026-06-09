using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly ITenantService _tenantService;

        public SmsService(
            HttpClient httpClient,
            ApplicationDbContext context,
            ITenantService tenantService)
        {
            _httpClient = httpClient;
            _context = context;
            _tenantService = tenantService;
        }

        public async Task SendSmsAsync(
            string PhoneNumber, string Message)
        {
            var campaign = new SmsCampaign
            {
                Id = Guid.NewGuid(),
                ChurchId = _tenantService.GetTenantId(),
                
                //CampaignName = dto.CampaignName,
                Message = Message,
                SentAt = DateTime.UtcNow
            };

            await _context.SmsCampaigns.AddAsync(campaign);

            
                await _httpClient.PostAsJsonAsync(
                    "https://api.ng.termii.com/api/sms/send",
                    new
                    {
                        to = PhoneNumber,
                        sms = Message
                    });

                

            await _context.SaveChangesAsync();
        }
    }
}
