using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.Teams;

public interface IService
{
    Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request);
    Task<string> InviteMember(Guid teamId, Request.InviteMemberRequest request);
    Task<BasePaginationResponse> GetMyTeams(PaginationRequest paginationRequest);
    Task<Response.TeamDetailResponse> GetTeamDetail(Guid teamId);
    Task<string> UpdateTeam(Guid teamId, Request.UpdateTeamRequest request);
    Task<string> RemoveMembers(Guid teamId, Request.RemoveMembersRequest request);
    Task<string> TransferLeader(Guid teamId, Request.TransferLeaderRequest request);
    Task<BasePaginationResponse> GetTeamRegisteredEvents(Guid teamId, RegisterTeams.Request.GetTeamRegisteredEventsRequest request, PaginationRequest paginationRequest);
    Task<Response.CountResponse> GetApprovedEventsCount(Guid teamId);
    Task<Response.LatestRegisteredEventResponse?> GetLatestRegisteredEvent(Guid teamId);
}
