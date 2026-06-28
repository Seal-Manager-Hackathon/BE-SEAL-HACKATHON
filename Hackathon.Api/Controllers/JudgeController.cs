using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JudgesService = Hackathon.Service.Judges;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.LecturerPolicy)]
[Route("api/v1/judge")]
public class JudgeController : ControllerBase
{
    private readonly JudgesService.IService _judgeService;

    public JudgeController(JudgesService.IService judgeService)
    {
        _judgeService = judgeService;
    }

    [HttpGet("tracks")]
    public async Task<IActionResult> GetMyTracks()
    {
        var result = await _judgeService.GetMyTracks();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("tracks/{trackId:guid}/submissions")]
    public async Task<IActionResult> GetTrackSubmissions(Guid trackId, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _judgeService.GetTrackSubmissions(trackId, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("submissions/{submissionId:guid}/criteria")]
    public async Task<IActionResult> GetSubmissionCriteria(Guid submissionId)
    {
        var result = await _judgeService.GetSubmissionCriteria(submissionId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("submissions/{submissionId:guid}/scores/me")]
    public async Task<IActionResult> GetMySubmissionScore(Guid submissionId)
    {
        var result = await _judgeService.GetMySubmissionScore(submissionId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("scores/me")]
    public async Task<IActionResult> GetMyScores([FromQuery] Guid eventId, [FromQuery] Guid? trackId, [FromQuery] bool? isGraded, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _judgeService.GetMyScores(eventId, trackId, isGraded, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpPost("submissions/{submissionId:guid}/scores")]
    public async Task<IActionResult> SubmitScore(Guid submissionId, [FromBody] JudgesService.Request.SubmitScoreRequest request)
    {
        var result = await _judgeService.SubmitScore(submissionId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "SCORE_SUBMITTED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("submissions/{submissionId:guid}/scores/mock")]
    public async Task<IActionResult> SubmitMockScore(Guid submissionId, [FromBody] JudgesService.Request.SubmitScoreRequest request)
    {
        var result = await _judgeService.SubmitMockScore(submissionId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "MOCK_SCORE_SUBMITTED", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("scores/{scoreId:guid}")]
    public async Task<IActionResult> UpdateScore(Guid scoreId, [FromBody] JudgesService.Request.SubmitScoreRequest request)
    {
        var result = await _judgeService.UpdateScore(scoreId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "SCORE_UPDATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("scores/{scoreId:guid}/finalize")]
    public async Task<IActionResult> FinalizeScore(Guid scoreId)
    {
        var result = await _judgeService.FinalizeScore(scoreId);
        return Ok(ApiResponseFactory.Base(result, 200, result, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPost("scores/{scoreId:guid}/retake")]
    public async Task<IActionResult> SubmitRetakeScore(Guid scoreId, [FromBody] JudgesService.Request.SubmitScoreRequest request)
    {
        var result = await _judgeService.SubmitRetakeScore(scoreId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "REGRADE_SCORE_SUBMITTED", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("events/{eventId:guid}/submissions")]
    public async Task<IActionResult> GetEventSubmissions(Guid eventId, [FromQuery] Guid? trackId, [FromQuery] Guid? roundId, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _judgeService.GetEventSubmissions(eventId, trackId, roundId, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("events/current/submissions/pending")]
    public async Task<IActionResult> GetCurrentEventPendingSubmissions([FromQuery] Guid? trackId, [FromQuery] Guid? roundId, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _judgeService.GetCurrentEventPendingSubmissions(trackId, roundId, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("events/{eventId:guid}/submissions/pending")]
    public async Task<IActionResult> GetPendingSubmissions(Guid eventId, [FromQuery] Guid? trackId, [FromQuery] Guid? roundId, [FromQuery] bool? isGraded, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _judgeService.GetPendingSubmissions(eventId, trackId, roundId, isGraded, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("events/{eventId:guid}/submissions/search")]
    public async Task<IActionResult> SearchSubmissions(Guid eventId, [FromQuery] Guid? trackId, [FromQuery] string? keyword, [FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _judgeService.SearchSubmissions(eventId, trackId, keyword, paginationRequest);
        result.TraceId = HttpContext.TraceIdentifier;
        return Ok(result);
    }

    [HttpGet("events/{eventId:guid}/teams")]
    public async Task<IActionResult> GetJudgeTeamsByEvent(Guid eventId, [FromQuery] Guid? roundId)
    {
        var (data, message) = await _judgeService.GetJudgeTeamsByEvent(eventId, roundId);
        return Ok(ApiResponseFactory.Base(data, 200, message, traceId: HttpContext.TraceIdentifier));
    }
}
