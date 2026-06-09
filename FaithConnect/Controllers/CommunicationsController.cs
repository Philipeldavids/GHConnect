using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.Controllers
{
    [ApiController]
    [Route("api/communications")]
    [Authorize]
    public class CommunicationsController
    : ControllerBase
    {
        private readonly
            ICommunicationService
                _communicationService;

        public CommunicationsController(
            ICommunicationService
                communicationService)
        {
            _communicationService =
                communicationService;
        }

        [HttpPost("sms")]
        public async Task<IActionResult>
    SendSms(BulkSmsDto dto)
        {
            await _communicationService
                .SendBulkSmsAsync(dto);

            return Ok();
        }

        [HttpPost("email")]
        public async Task<IActionResult>
    SendEmail(BulkEmailDto dto)
        {
            await _communicationService
                .SendBulkEmailAsync(dto);

            return Ok();
        }

        [HttpGet("history")]
        public async Task<IActionResult>
    History()
        {
            return Ok(
                await _communicationService
                    .GetHistoryAsync());
        }

        [HttpGet("{memberId}/member-history")]
        public async Task<IActionResult> MemberHistory(Guid memberId)
        {
            return Ok(await _communicationService.GetMemberHistoryAsync(memberId));
        }
    }
}
