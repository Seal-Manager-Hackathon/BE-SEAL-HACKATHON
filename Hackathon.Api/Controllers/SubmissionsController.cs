using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmissionsService = Hackathon.Service.Submissions;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/submissions")]
public class SubmissionsController : ControllerBase
{
    private readonly SubmissionsService.IService _submissionsService;

    public SubmissionsController(SubmissionsService.IService submissionsService)
    {
        _submissionsService = submissionsService;
    }

    [HttpGet("{submissionId:guid}")]
    public async Task<IActionResult> GetSubmissionDetail(Guid submissionId)
    {
        var result = await _submissionsService.GetSubmissionDetail(submissionId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("rounds/{roundId:guid}/register-teams/{registerTeamId:guid}")]
    public async Task<IActionResult> GetSubmissions(Guid roundId, Guid registerTeamId, [FromQuery] SubmissionsService.Request.GetSubmissionsRequest request)
    {
        var result = await _submissionsService.GetSubmissions(roundId, registerTeamId, request);
        return Ok(result);
    }

    [HttpPost("rounds/{roundId:guid}/register-teams/{registerTeamId:guid}")]
    public async Task<IActionResult> SubmitRoundProject(Guid roundId, Guid registerTeamId, [FromBody] SubmissionsService.Request.SubmitRoundProjectRequest request)
    {
        var result = await _submissionsService.SubmitRoundProject(roundId, registerTeamId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "SUBMISSION_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
