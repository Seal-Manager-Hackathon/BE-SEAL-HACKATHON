using Hackathon.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationsService = Hackathon.Service.Notifications;

namespace Hackathon.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationsService.IService _notificationsService;

    public NotificationsController(NotificationsService.IService notificationsService)
    {
        _notificationsService = notificationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications([FromQuery] string? status, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _notificationsService.GetNotifications(status, pageIndex, pageSize);
        return Ok(ApiResponseFactory.BasePagination(result.Items, pageIndex, pageSize, result.TotalCount));
    }
}
