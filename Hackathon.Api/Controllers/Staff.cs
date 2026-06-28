using Hackathon.Api.Extention;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoundsService = Hackathon.Service.Rounds;
using TracksService = Hackathon.Service.Tracks;
using AssignEventsService = Hackathon.Service.AssignEvents;
using AssignTracksService = Hackathon.Service.AssignTracks;
using StaffService = Hackathon.Service.Staff;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;

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
    private readonly StaffService.IService _staffService;
    private readonly RegisterTeamsService.IService _registerTeamsService;

    public Staff(TracksService.IService tracksService, AssignEventsService.IService assignEventsService, AssignTracksService.IService assignTracksService, RoundsService.IService roundsService, StaffService.IService staffService, RegisterTeamsService.IService registerTeamsService)
    {
        _tracksService = tracksService;
        _roundsService = roundsService;
        _assignEventsService = assignEventsService;
        _assignTracksService = assignTracksService;
        _staffService = staffService;
        _registerTeamsService = registerTeamsService;
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

    [HttpGet("events/{eventId:guid}/assignments")]
    public async Task<IActionResult> GetEventAssignments(Guid eventId, [FromQuery] EventRoleEnum? eventRole, [FromQuery] string? keyword, [FromQuery] Guid? trackId, [FromQuery] bool? isDisable, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _assignEventsService.GetEventAssignments(eventId, eventRole, keyword, trackId, isDisable, paginationRequest);
        return Ok(result);
    }

    [HttpGet("events/{eventId:guid}/lecturers/available")]
    public async Task<IActionResult> GetAvailableLecturers(Guid eventId, [FromQuery] AssignEventsService.Request.GetAvailableLecturersRequest request)
    {
        var result = await _assignEventsService.GetAvailableLecturers(eventId, request);
        return Ok(result);
    }

    [HttpPost("events/{eventId:guid}/assign-lecturers")]
    public async Task<IActionResult> AssignLecturerToEvent(Guid eventId, [FromBody] AssignEventsService.Request.AssignLecturerRequest request)
    {
        var result = await _assignEventsService.AssignLecturerToEvent(eventId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "LECTURER_ASSIGNED_TO_EVENT_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("events/{eventId:guid}/tracks/{trackId:guid}/assign-lecturers")]
    public async Task<IActionResult> AssignLecturerToTrack(Guid eventId, Guid trackId, [FromBody] AssignTracksService.Request.AssignJudgeRequest request)
    {
        var result = await _assignTracksService.AssignLecturerToTrack(eventId, trackId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "LECTURER_ASSIGNED_TO_TRACK_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("events/{eventId:guid}/tracks/{trackId:guid}/lecturers")]
    public async Task<IActionResult> GetLecturersAssignedToTrack(Guid eventId, Guid trackId, [FromQuery] bool? isDisable)
    {
        var result = await _assignTracksService.GetLecturersAssignedToTrack(eventId, trackId, isDisable);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("assign-events/{id:guid}")]
    public async Task<IActionResult> RemoveLecturerAssignment(Guid id)
    {
        var result = await _assignEventsService.RemoveLecturerAssignment(id);
        return Ok(ApiResponseFactory.Base(new { id = result }, 200, "LECTURER_ASSIGNMENT_REMOVED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("assign-tracks/{id:guid}")]
    public async Task<IActionResult> RemoveLecturerFromTrack(Guid id)
    {
        var result = await _assignTracksService.RemoveLecturerFromTrack(id);
        return Ok(ApiResponseFactory.Base(new { id = result }, 200, "LECTURER_REMOVED_FROM_TRACK_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpGet("events")]
    public async Task<IActionResult> GetStaffEvents([FromQuery] PaginationRequest request)
    {
        var result = await _staffService.GetStaffEvents(request);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpGet("events/search")]
    public async Task<IActionResult> SearchStaffEvents([FromQuery] StaffService.Request.SearchStaffEventsRequest request)
    {
        var result = await _staffService.SearchStaffEvents(request);
        return Ok(result);
    }

    [Authorize(Policy = JwtExtensions.StaffPolicy)]
    [HttpGet("events/current")]
    public async Task<IActionResult> GetCurrentStaffEvents()
    {
        var result = await _staffService.GetCurrentStaffEvents();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("events/{eventId:guid}/register-teams")]
    public async Task<IActionResult> GetRegisterTeamsByEvent(Guid eventId, [FromQuery] string? keyword, [FromQuery] RegisterTeamStatusEnum? status, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _registerTeamsService.GetRegisterTeamsByEvent(eventId, keyword, status, null, paginationRequest);
        return Ok(result);
    }
}
