using Hackathon.Api.Extention;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/register-teams")]
public class RegisterTeamController : ControllerBase
{
    private readonly RegisterTeamsService.IService _registerTeamService;

    public RegisterTeamController(RegisterTeamsService.IService registerTeamService)
    {
        _registerTeamService = registerTeamService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEvent([FromBody] RegisterTeamsService.Request.RegisterEventRequest request)
    {
        var (data, message) = await _registerTeamService.RegisterEvent(request);
        return Ok(ApiResponseFactory.Base(data,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyRegisteredEvents([FromQuery] RegisterTeamsService.Request.GetMyRegisteredEventsRequest request, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamService.GetMyRegisteredEvents(request, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{registerId:guid}/rejection-reason")]
    public async Task<IActionResult> GetRejectionReason(Guid registerId)
    {
        var result = await _registerTeamService.GetRejectionReason(registerId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("staff/events/{eventId:guid}")]
    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    public async Task<IActionResult> GetRegisterTeamsByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] RegisterTeamStatusEnum? status, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamService.GetRegisterTeamsByEvent(eventId, keyword, status, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("staff/{registerTeamId:guid}")]
    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    public async Task<IActionResult> GetRegisterTeamDetail(Guid registerTeamId)
    {
        var result = await _registerTeamService.GetRegisterTeamDetail(registerTeamId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("staff/{registerId:guid}/approve")]
    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    public async Task<IActionResult> ApproveRegistration(Guid registerId)
    {
        var result = await _registerTeamService.AcceptRegisterTeam(registerId);
        return Ok(ApiResponseFactory.Base(result,200,"REGISTER_TEAM_ACCEPTED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPut("staff/{registerId:guid}/reject")]
    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    public async Task<IActionResult> RejectRegistration(Guid registerId, [FromBody] RegisterTeamsService.Request.RejectRegisterTeamRequest request)
    {
        var result = await _registerTeamService.RejectRegisterTeam(registerId, request);
        return Ok(ApiResponseFactory.Base(result,200,"REGISTER_TEAM_REJECTED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
