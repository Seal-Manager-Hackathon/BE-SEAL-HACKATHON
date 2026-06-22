using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicsService = Hackathon.Service.Topics;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
[Route("api/v1/admin/tracks")]
public class AdminTracksTopicsController : ControllerBase
{
    private readonly TopicsService.IService _topicsService;

    public AdminTracksTopicsController(TopicsService.IService topicsService)
    {
        _topicsService = topicsService;
    }

    [HttpPost("{trackId:guid}/topics")]
    public async Task<IActionResult> CreateTopic(Guid trackId, [FromBody] TopicsService.Request.CreateTopicRequest request)
    {
        var result = await _topicsService.CreateTopic(trackId, request);
        return Created("", ApiResponseFactory.Base(result, 201, "TOPIC_CREATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
