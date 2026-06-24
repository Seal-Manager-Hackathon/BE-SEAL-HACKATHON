using Hackathon.Service.Models;

namespace Hackathon.Service.Events;

public interface IService
{
    Task<BasePaginationResponse> GetEvents(Request.GetEventsRequest request);
    Task<BasePaginationResponse> GetEventsForAdmin(Request.GetEventsForAdminRequest request);
    Task<Response.CreateEventResponse> CreateEvent(Request.CreateEventRequest request);
    Task<Response.AssignStaffToEventResponse> AssignStaffToEvent(Guid eventId, Request.AssignStaffToEventRequest request);
    Task<string> UpdateEvent(Guid eventId, Request.UpdateEventRequest request);
    Task<string> DeleteEvent(Guid eventId);
    Task<string> DeleteAward(Guid awardId);
    Task<Guid> RemoveTrackAssignment(Guid assignTrackId);
    Task<string> PublishEvent(Guid eventId);
    Task<Response.EventResponse> GetEvent(Guid eventId);
    Task<BasePaginationResponse> GetJoinedEvents(Request.GetJoinedEventsRequest request);
    Task<List<Response.EventParticipantResponse>> GetMostParticipants(int? limit, bool? isDisable);
}
