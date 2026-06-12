namespace Hackathon.Service.MentorNotifications;

public interface IService
{
    Task<(List<Response.MentorNotificationResponse> Items, int TotalCount)> GetMentorNotifications(Guid? eventId, Guid? trackId, int pageIndex, int pageSize);
}
