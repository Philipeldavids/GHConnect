using FaithConnect.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaithConnect.API.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(
        IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        try
        {
            var result =
            await _dashboardService
                .GetSummaryAsync();

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}