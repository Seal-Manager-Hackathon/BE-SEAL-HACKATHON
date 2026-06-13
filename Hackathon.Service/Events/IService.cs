namespace Hackathon.Service.Events;

public interface IService
{
    Task<List<Response.EventResponse>> GetEvents(int? year, bool? isDisable);
    Task<Response.EventResponse> GetEvent(Guid eventId, bool? isDisable);
    Task<(List<Response.EventResponse> Items, int TotalCount)> SearchEvents(string? keyword, int? year, string? status, bool? isDisable, int pageIndex, int pageSize);
    Task<List<Response.EventResponse>> GetJoinedEvents(int? year, string? status, bool? isDisable);
    Task<List<Response.EventParticipantResponse>> GetMostParticipants(int? limit, bool? isDisable);
}
