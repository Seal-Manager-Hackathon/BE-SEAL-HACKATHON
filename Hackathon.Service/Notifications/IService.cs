namespace Hackathon.Service.Notifications;

public interface IService
{
    Task<(List<Response.NotificationResponse> Items, int TotalCount)> GetNotifications(string? status, int pageIndex, int pageSize);
}
