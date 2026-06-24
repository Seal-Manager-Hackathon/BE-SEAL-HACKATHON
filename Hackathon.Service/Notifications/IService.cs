using Hackathon.Service.Models;

namespace Hackathon.Service.Notifications;

public interface IService
{
    Task<BasePaginationResponse> GetMyNotifications(PaginationRequest paginationRequest);
    Task<string> MarkAsRead(Guid notificationId);
    Task<string> MarkAllAsRead();
}
