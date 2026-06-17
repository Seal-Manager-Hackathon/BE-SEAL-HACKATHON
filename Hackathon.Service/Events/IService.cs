using Hackathon.Service.Models;

namespace Hackathon.Service.Events;

public interface IService
{
    Task<BasePaginationResponse> GetEvents(Request.GetEventsRequest request);
    Task<BasePaginationResponse> GetEventsForAdmin(Request.GetEventsForAdminRequest request);
    Task<Response.EventResponse> GetEvent(Guid eventId, bool? isDisable);
    Task<List<Response.EventResponse>> GetJoinedEvents(int? year, string? status, bool? isDisable);
    Task<List<Response.EventParticipantResponse>> GetMostParticipants(int? limit, bool? isDisable);
}
