using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MentorNotificationsService = Hackathon.Service.MentorNotifications;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/mentor-notifications")]
public class MentorNotificationsController : ControllerBase
{
    private readonly MentorNotificationsService.IService _mentorNotificationsService;

    public MentorNotificationsController(MentorNotificationsService.IService mentorNotificationsService)
    {
        _mentorNotificationsService = mentorNotificationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMentorNotifications([FromQuery] Guid? eventId, [FromQuery] Guid? trackId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mentorNotificationsService.GetMentorNotifications(eventId, trackId, pageIndex, pageSize);
        return Ok(ApiResponseFactory.BasePagination(result.Items, pageIndex, pageSize, result.TotalCount));
    }
}
