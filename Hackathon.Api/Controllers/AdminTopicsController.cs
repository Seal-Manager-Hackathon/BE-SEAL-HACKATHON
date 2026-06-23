using Hackathon.Api.Extention;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicsService = Hackathon.Service.Topics;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize(Policy = JwtExtensions.StaffOrAdminPolicy)]
[Route("api/v1/admin/topics")]
public class AdminTopicsController : ControllerBase
{
    private readonly TopicsService.IService _topicsService;

    public AdminTopicsController(TopicsService.IService topicsService)
    {
        _topicsService = topicsService;
    }

    [HttpPatch("{topicId:guid}")]
    public async Task<IActionResult> UpdateTopic(Guid topicId, [FromBody] TopicsService.Request.UpdateTopicRequest request)
    {
        var result = await _topicsService.UpdateTopic(topicId, request);
        return Ok(ApiResponseFactory.Base(result, 200, "TOPIC_UPDATED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }

    [HttpDelete("{topicId:guid}")]
    public async Task<IActionResult> DeleteTopic(Guid topicId)
    {
        var result = await _topicsService.DeleteTopic(topicId);
        return Ok(ApiResponseFactory.Base(result, 200, "TOPIC_DELETED_SUCCESSFULLY", traceId: HttpContext.TraceIdentifier));
    }
}
