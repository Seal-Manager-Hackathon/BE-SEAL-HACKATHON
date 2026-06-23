using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.Tracks;

public interface IService
{
    Task<BasePaginationResponse> GetTracksByEvent(Guid eventId, string? keyword, bool? isDisable, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> GetTopicsByTrack(Guid trackId, string? keyword, bool? isDisable, PaginationRequest paginationRequest);
    Task<Response.TeamTrackAssignmentResponse> AssignTrackToTeam(Guid teamId, Request.AssignTrackToTeamRequest request);
    Task<Response.TeamTopicAssignmentResponse> AssignTopicToTeam(Guid teamId, Request.AssignTopicToTeamRequest request);
    Task<BasePaginationResponse> GetApprovedTeamsByEvent(Guid eventId, string? keyword, RegisterTeamStatusEnum? status, bool? isDisable, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> GetTracks(Guid? eventId, string? keyword, bool? isDisable, PaginationRequest paginationRequest);
    Task<Response.TrackTeamCountResponse> GetTrackTeamCount(Guid trackId);
}
