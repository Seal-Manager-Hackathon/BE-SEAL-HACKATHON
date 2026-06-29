using Hackathon.Api.Extention;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamsService = Hackathon.Service.Teams;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/teams")]
public class TeamController(TeamsService.IService teamService) : ControllerBase
{
    private readonly TeamsService.IService _teamService = teamService;

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyTeams([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetMyTeams(paginationRequest);
        return Ok(result);
    }

    [HttpGet("{teamId:guid}")]
    public async Task<IActionResult> GetTeamDetail(Guid teamId)
    {
        var result = await _teamService.GetTeamDetail(teamId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPost]
    public async Task<IActionResult> CreateTeam(TeamsService.Request.CreateTeamRequest request)
    {
        var result = await _teamService.CreateTeam(request);
        return Ok(ApiResponseFactory.Base(result,201,"TEAM_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPost("{teamId:guid}/invitations")]
    public async Task<IActionResult> InviteMember(Guid teamId, TeamsService.Request.InviteMemberRequest request)
    {
        var message = await _teamService.InviteMember(teamId, request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPut("{teamId:guid}")]
    public async Task<IActionResult> UpdateTeam(Guid teamId, TeamsService.Request.UpdateTeamRequest request)
    {
        var message = await _teamService.UpdateTeam(teamId, request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpDelete("{teamId:guid}/members")]
    public async Task<IActionResult> RemoveMembers(Guid teamId, TeamsService.Request.RemoveMembersRequest request)
    {
        var message = await _teamService.RemoveMembers(teamId, request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPut("{teamId:guid}/leader")]
    public async Task<IActionResult> TransferLeader(Guid teamId, TeamsService.Request.TransferLeaderRequest request)
    {
        var message = await _teamService.TransferLeader(teamId, request);
        return Ok(ApiResponseFactory.Base(null,200,message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpGet("{teamId:guid}/events")]
    public async Task<IActionResult> GetTeamRegisteredEvents(Guid teamId, [FromQuery] RegisterTeamsService.Request.GetTeamRegisteredEventsRequest request, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetTeamRegisteredEvents(teamId, request, paginationRequest);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpGet("{teamId:guid}/events/approved-count")]
    public async Task<IActionResult> GetApprovedEventsCount(Guid teamId)
    {
        var result = await _teamService.GetApprovedEventsCount(teamId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpGet("{teamId:guid}/events/latest")]
    public async Task<IActionResult> GetLatestRegisteredEvent(Guid teamId)
    {
        var result = await _teamService.GetLatestRegisteredEvent(teamId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpGet("my-registrations")]
    public async Task<IActionResult> GetMyRegistrationsByEvent([FromQuery] TeamsService.Request.GetMyRegistrationsByEventRequest request)
    {
        var result = await _teamService.GetMyRegistrationsByEvent(request);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet("/api/v1/admin/teams")]
    public async Task<IActionResult> GetAdminTeams([FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetAdminTeams(keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("{teamId:guid}/members")]
    public async Task<IActionResult> GetTeamMembers(Guid teamId)
    {
        var result = await _teamService.GetTeamMembers(teamId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("{teamId:guid}/notifications")]
    public async Task<IActionResult> GetTeamNotifications(Guid teamId)
    {
        var result = await _teamService.GetTeamNotifications(teamId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpGet("me/register-teams")]
    public async Task<IActionResult> GetMyTeamRegisterEvents([FromQuery] string? status, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _teamService.GetMyTeamRegisterEvents(status, paginationRequest);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpPatch("/api/v1/admin/teams/{teamId:guid}/disable")]
    public async Task<IActionResult> DisableTeam(Guid teamId)
    {
        var message = await _teamService.DisableTeam(teamId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("{teamId:guid}/lock")]
    public async Task<IActionResult> LockTeam(Guid teamId)
    {
        var message = await _teamService.LockTeam(teamId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("{teamId:guid}/unlock")]
    public async Task<IActionResult> UnlockTeam(Guid teamId)
    {
        var message = await _teamService.UnlockTeam(teamId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPost("{teamId:guid}/leave")]
    public async Task<IActionResult> LeaveTeam(Guid teamId)
    {
        var message = await _teamService.LeaveTeam(teamId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPost("{teamId:guid}/rounds/{roundId:guid}/appeal")]
    public async Task<IActionResult> AppealRound(Guid teamId, Guid roundId, [FromBody] TeamsService.Request.RoundAppealRequest request)
    {
        var result = await _teamService.AppealRound(teamId, roundId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "APPEAL_SUBMITTED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StudentPolicy)]
    [HttpPost("{teamId:guid}/submissions/{submissionId:guid}/appeal")]
    public async Task<IActionResult> AppealSubmission(Guid teamId, Guid submissionId, [FromBody] TeamsService.Request.SubmissionAppealRequest request)
    {
        var result = await _teamService.AppealSubmission(teamId, submissionId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "APPEAL_SUBMITTED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
