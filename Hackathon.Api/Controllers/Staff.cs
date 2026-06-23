using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoundsService = Hackathon.Service.Rounds;
using TracksService = Hackathon.Service.Tracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
[Route("api/v1/staff")]
public class Staff : ControllerBase
{
    private readonly TracksService.IService _tracksService;
    private readonly RoundsService.IService _roundsService;

    public Staff(TracksService.IService tracksService, RoundsService.IService roundsService)
    {
        _tracksService = tracksService;
        _roundsService = roundsService;
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpGet("events/{eventId:guid}/tracks")]
    public async Task<IActionResult> GetTracksByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTracksByEvent(eventId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpGet("tracks/{trackId:guid}/topics")]
    public async Task<IActionResult> GetTopicsByTrack(Guid trackId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetTopicsByTrack(trackId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("events/{eventId:guid}/teams")]
    public async Task<IActionResult> GetApprovedTeamsByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetApprovedTeamsByEvent(eventId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("rounds/{roundId:guid}/submissions")]
    public async Task<IActionResult> GetRoundSubmissions(Guid roundId, [FromQuery] RoundsService.Request.GetStaffRoundSubmissionsQuery query)
    {
        var result = await _roundsService.GetStaffRoundSubmissions(roundId, query);
        return Ok(result);
    }

    [HttpPost("submissions/{submissionId:guid}/assign-judges")]
    public async Task<IActionResult> AssignJudgesToSubmission(Guid submissionId, RoundsService.Request.AssignJudgesToSubmissionRequest request)
    {
        var result = await _roundsService.AssignJudgesToSubmission(submissionId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "JUDGES_ASSIGNED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpPatch("teams/{teamId:guid}/track")]
    public async Task<IActionResult> AssignTrackToTeam(Guid teamId, TracksService.Request.AssignTrackToTeamRequest request)
    {
        var result = await _tracksService.AssignTrackToTeam(teamId, request);
        return Ok(ApiResponseFactory.Base(result, 200,"TRACK_ASSIGNED_TO_TEAM_SUCCESSFULLY",traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpPatch("teams/{teamId:guid}/topic")]
    public async Task<IActionResult> AssignTopicToTeam(Guid teamId, TracksService.Request.AssignTopicToTeamRequest request)
    {
        var result = await _tracksService.AssignTopicToTeam(teamId, request);
        return Ok(ApiResponseFactory.Base(result, 200,"TOPIC_ASSIGNED_TO_TEAM_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
