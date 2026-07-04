using Hackathon.Service.Models;

namespace Hackathon.Service.Events;

public interface IService
{
    Task<BasePaginationResponse> GetEvents(Request.GetEventsRequest request);
    Task<BasePaginationResponse> GetEventsForAdmin(Request.GetEventsForAdminRequest request);
    Task<BasePaginationResponse> GetEventAssignments(Guid eventId, PaginationRequest paginationRequest);
    Task<Response.SetupStatusResponse> GetSetupStatus(Guid eventId);
    Task<string> RemoveStaffAssignment(Guid assignEventId);
    Task<BasePaginationResponse> GetAvailableStaff(Guid eventId, string? keyword, PaginationRequest paginationRequest);
    Task<List<Response.AwardResponse>> GetAwards(Guid eventId);
    Task<List<Response.LeaderboardResponse>> GetLeaderboard(Guid eventId);
    Task<Response.EventSummaryResponse> GetSummary(Guid eventId);
    Task<List<Response.TeamScoreResponse>> GetTeamScores(Guid eventId, Guid teamId);
    Task<Response.CreateEventResponse> CreateEvent(Request.CreateEventRequest request);
    Task<Response.AssignStaffToEventResponse> AssignStaffToEvent(Guid eventId, Request.AssignStaffToEventRequest request);
    Task<Response.CreateAwardResponse> CreateAward(Guid eventId, Request.CreateAwardRequest request);
    Task<Response.AssignEventToTrackResponse> AssignEventToTrack(Guid assignEventId, Request.AssignEventToTrackRequest request);
    Task<string> RecalculateLeaderboard(Guid eventId);
    Task<string> UpdateEvent(Guid eventId, Request.UpdateEventRequest request);
    Task<string> DeleteEvent(Guid eventId);
    Task<string> DeleteAward(Guid awardId);
    Task<Guid> RemoveTrackAssignment(Guid assignTrackId);
    Task<string> UpdateAward(Guid id, Request.UpdateAwardRequest request);
    Task<string> CloseEvent(Guid eventId);
    Task<string> RestoreEvent(Guid eventId);
    Task<string> UnpublishEvent(Guid eventId);
    Task<string> UpdateLecturerRole(Guid id, Request.UpdateLecturerRoleRequest request);
    Task<string> LockLeaderboard(Guid eventId);
    Task<string> PublishLeaderboard(Guid eventId);
    Task<string> PublishEvent(Guid eventId);
    Task<Response.EventResponse> GetEvent(Guid eventId);
    Task<Response.EventResponse> GetAdminEvent(Guid eventId);
    Task<BasePaginationResponse> GetJoinedEvents(Request.GetJoinedEventsRequest request);
    Task<List<Response.EventParticipantResponse>> GetMostParticipants(int? limit, bool? isDisable);
}
