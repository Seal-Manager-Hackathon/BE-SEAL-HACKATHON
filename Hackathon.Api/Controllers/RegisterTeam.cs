using Hackathon.Api.Extention;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterTeamService = Hackathon.Service.RegisterTeam;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
[Route("api/v1/staff")]
public class RegisterTeam : ControllerBase
{
    private readonly RegisterTeamService.IService _registerTeamService;

    public RegisterTeam(RegisterTeamService.IService registerTeamService)
    {
        _registerTeamService = registerTeamService;
    }

    [HttpGet("events/{eventId:guid}/register-teams")]
    public async Task<IActionResult> GetRegisterTeamsByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] RegisterTeamStatusEnum? status, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamService.GetRegisterTeamsByEvent(eventId, keyword, status, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("register-teams/{registerTeamId:guid}")]
    public async Task<IActionResult> GetRegisterTeamDetail(Guid registerTeamId)
    {
        var result = await _registerTeamService.GetRegisterTeamDetail(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpPatch("register-teams/{registerTeamId:guid}/accept")]
    public async Task<IActionResult> AcceptRegisterTeam(Guid registerTeamId)
    {
        var result = await _registerTeamService.AcceptRegisterTeam(registerTeamId);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpPatch("register-teams/{registerTeamId:guid}/reject")]
    public async Task<IActionResult> RejectRegisterTeam(Guid registerTeamId, RegisterTeamService.Request.RejectRegisterTeamRequest request)
    {
        var result = await _registerTeamService.RejectRegisterTeam(registerTeamId, request);
        return Ok(ApiResponseFactory.Base(result, traceId: HttpContext.TraceIdentifier));
    }
}
