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
}
