using Hackathon.Service.Models;

namespace Hackathon.Service.Tracks;

public interface IService
{
    Task<BasePaginationResponse> GetTracks(Guid? eventId, string? keyword, bool? isDisable, int pageIndex, int pageSize);
    Task<BasePaginationResponse> GetTracksByEvent(Guid eventId, string? keyword, bool? isDisable, int pageIndex, int pageSize);
    Task<BasePaginationResponse> GetTopicsByTrack(Guid trackId, string? keyword, bool? isDisable, int pageIndex, int pageSize);
    Task<Response.TeamTrackAssignmentResponse> AssignTrackToTeam(Guid teamId, Request.AssignTrackToTeamRequest request);
    Task<Response.TeamTopicAssignmentResponse> AssignTopicToTeam(Guid teamId, Request.AssignTopicToTeamRequest request);
}
