using Hackathon.Service.Models;
using Hackathon.Service.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly IService _notificationsService;

    public NotificationsController(IService notificationsService)
    {
        _notificationsService = notificationsService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyNotifications([FromQuery] PaginationRequest paginationRequest)
    {
        var result = await _notificationsService.GetMyNotifications(paginationRequest);
        return Ok(result);
    }

    [HttpPatch("{notificationId:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid notificationId)
    {
        var message = await _notificationsService.MarkAsRead(notificationId);
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpGet("me/unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var count = await _notificationsService.GetUnreadCount();
        return Ok(ApiResponseFactory.Base(new { unreadCount = count }, 200, "SUCCESS", traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var message = await _notificationsService.MarkAllAsRead();
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }

    [HttpPatch("all/disable")]
    public async Task<IActionResult> DisableAll()
    {
        var message = await _notificationsService.DisableAll();
        return Ok(ApiResponseFactory.Base(null, 200, message, traceId: HttpContext.TraceIdentifier));
    }
}
