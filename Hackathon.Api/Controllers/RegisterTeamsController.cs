using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/register-teams")]
public class RegisterTeamsController : ControllerBase
{
    private readonly RegisterTeamsService.IService _registerTeamsService;

    public RegisterTeamsController(RegisterTeamsService.IService registerTeamsService)
    {
        _registerTeamsService = registerTeamsService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEvent([FromBody] RegisterTeamsService.Request.RegisterEventRequest request)
    {
        var result = await _registerTeamsService.RegisterEvent(request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyRegisteredEvents([FromQuery] RegisterTeamsService.Request.GetMyRegisteredEventsRequest request, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamsService.GetMyRegisteredEvents(request, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{registerId:guid}/rejection-reason")]
    public async Task<IActionResult> GetRejectionReason(Guid registerId)
    {
        var result = await _registerTeamsService.GetRejectionReason(registerId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
