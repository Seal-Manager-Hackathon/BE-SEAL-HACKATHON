using Hackathon.Api.Extention;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoundsService = Hackathon.Service.Rounds;
using TracksService = Hackathon.Service.Tracks;
using AssignEventsService = Hackathon.Service.AssignEvents;
using AssignTracksService = Hackathon.Service.AssignTracks;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
[Route("api/v1/staff")]
public class Staff : ControllerBase
{
    private readonly TracksService.IService _tracksService;
    private readonly AssignEventsService.IService _assignEventsService;
    private readonly AssignTracksService.IService _assignTracksService;
    private readonly RoundsService.IService _roundsService;

    public Staff(TracksService.IService tracksService, AssignEventsService.IService assignEventsService, AssignTracksService.IService assignTracksService, RoundsService.IService roundsService)
    {
        _tracksService = tracksService;
        _roundsService = roundsService;
        _assignEventsService = assignEventsService;
        _assignTracksService = assignTracksService;
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
    public async Task<IActionResult> GetApprovedTeamsByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] RegisterTeamStatusEnum? status, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _tracksService.GetApprovedTeamsByEvent(eventId, keyword, status, isDisable, paginationRequest);
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
    [HttpPatch("events/{eventId:guid}/teams/{teamId:guid}/track")]
    public async Task<IActionResult> AssignTrackToTeam(Guid eventId, Guid teamId, TracksService.Request.AssignTrackToTeamRequest request)
    {
        var result = await _tracksService.AssignTrackToTeam(eventId, teamId, request);
        return Ok(ApiResponseFactory.Base(result, 200,"TRACK_ASSIGNED_TO_TEAM_SUCCESSFULLY",traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpPatch("events/{eventId:guid}/teams/{teamId:guid}/topic")]
    public async Task<IActionResult> AssignTopicToTeam(Guid eventId, Guid teamId, TracksService.Request.AssignTopicToTeamRequest request)
    {
        var result = await _tracksService.AssignTopicToTeam(eventId, teamId, request);
        return Ok(ApiResponseFactory.Base(result, 200,"TOPIC_ASSIGNED_TO_TEAM_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("events/{eventId:guid}/lecturers")]
    public async Task<IActionResult> GetAssignedLecturersByEvent(Guid eventId, [FromQuery] Guid? eventRoleId, [FromQuery] string? keyword, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _assignEventsService.GetAssignedLecturersByEvent(eventId, eventRoleId, keyword, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpPost("events/{eventId:guid}/assign-lecturers")]
    public async Task<IActionResult> AssignLecturerToEvent(Guid eventId, [FromBody] AssignEventsService.Request.AssignLecturerRequest request)
    {
        var result = await _assignEventsService.AssignLecturerToEvent(eventId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("tracks/{trackId:guid}/assign-judges")]
    public async Task<IActionResult> AssignJudgeToTrack(Guid trackId, [FromBody] AssignTracksService.Request.AssignJudgeRequest request)
    {
        var result = await _assignTracksService.AssignJudgeToTrack(trackId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "JUDGE_ASSIGNED_TO_TRACK_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("assign-events/{id:guid}")]
    public async Task<IActionResult> RemoveLecturerAssignment(Guid id)
    {
        var result = await _assignEventsService.RemoveLecturerAssignment(id);
        return Ok(ApiResponseFactory.Base(new { id = result }, 200, "LECTURER_ASSIGNMENT_REMOVED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
