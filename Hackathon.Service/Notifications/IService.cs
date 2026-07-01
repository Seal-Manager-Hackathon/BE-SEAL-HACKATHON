using Hackathon.Service.Models;

namespace Hackathon.Service.Notifications;

public interface IService
{
    Task<BasePaginationResponse> GetMyNotifications(PaginationRequest paginationRequest);
    Task<int> GetUnreadCount();
    Task<string> MarkAsRead(Guid notificationId);
    Task<string> MarkAllAsRead();
    Task<string> DisableAll();
}
