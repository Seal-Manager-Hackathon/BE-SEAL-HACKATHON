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
    public async Task<IActionResult> GetTrackSubmissions(Guid trackId)
    {
        var result = await _judgeService.GetTrackSubmissions(trackId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
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
    public async Task<IActionResult> GetMyScores()
    {
        var result = await _judgeService.GetMyScores();
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
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

    [HttpGet("events/{eventId:guid}/teams")]
    public async Task<IActionResult> GetJudgeTeamsByEvent(Guid eventId, [FromQuery] Guid? roundId)
    {
        var (data, message) = await _judgeService.GetJudgeTeamsByEvent(eventId, roundId);
        return Ok(ApiResponseFactory.Base(data, 200, message, traceId: HttpContext.TraceIdentifier));
    }
}
