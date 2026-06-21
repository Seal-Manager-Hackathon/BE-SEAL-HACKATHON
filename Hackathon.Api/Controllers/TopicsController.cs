using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using TopicsService = Hackathon.Service.Topics;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("api/v1/events/{eventId:guid}/register-teams/{registerTeamId:guid}/topic")]
public class TopicsController : ControllerBase
{
    private readonly TopicsService.IService _topicsService;

    public TopicsController(TopicsService.IService topicsService)
    {
        _topicsService = topicsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTopic(Guid eventId, Guid registerTeamId)
    {
        var result = await _topicsService.GetTopic(eventId, registerTeamId);
        return Ok(ApiResponseFactory.Base(result,200,"SUCCESS", traceId: HttpContext.TraceIdentifier));
    }
}
