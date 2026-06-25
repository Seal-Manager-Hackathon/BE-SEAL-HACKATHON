using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicsService = Hackathon.Service.Topics;

namespace Hackathon.Api.Controllers;

[ApiController]
public class TopicsController : ControllerBase
{
    private readonly TopicsService.IService _topicsService;

    public TopicsController(TopicsService.IService topicsService)
    {
        _topicsService = topicsService;
    }

    [HttpGet("api/v1/events/{eventId:guid}/register-teams/{registerTeamId:guid}/topic")]
    public async Task<IActionResult> GetTopic(Guid eventId, Guid registerTeamId)
    {
        var result = await _topicsService.GetTopic(eventId, registerTeamId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("api/v1/topics/{topicId:guid}")]
    public async Task<IActionResult> GetTopicDetail(Guid topicId)
    {
        var result = await _topicsService.GetTopicDetail(topicId);
        return Ok(ApiResponseFactory.Base(result, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpPatch("/api/v1/admin/topics/{topicId:guid}")]
    public async Task<IActionResult> UpdateTopic(Guid topicId, [FromBody] TopicsService.Request.UpdateTopicRequest request)
    {
        var result = await _topicsService.UpdateTopic(topicId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "TOPIC_UPDATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
    [HttpDelete("/api/v1/admin/topics/{topicId:guid}")]
    public async Task<IActionResult> DeleteTopic(Guid topicId)
    {
        var result = await _topicsService.DeleteTopic(topicId);
        return Ok(ApiResponseFactory.Base(result, 200, "TOPIC_DELETED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
