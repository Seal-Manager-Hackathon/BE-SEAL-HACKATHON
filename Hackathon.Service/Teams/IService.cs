using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.Teams;

public interface IService
{
    Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request);
    Task<Response.MessageResponse> InviteMember(Guid teamId, Request.InviteMemberRequest request);
    Task<BasePaginationResponse> GetMyTeams(PaginationRequest paginationRequest);
    Task<Response.TeamDetailResponse> GetTeamDetail(Guid teamId);
    Task<Response.MessageResponse> UpdateTeam(Guid teamId, Request.UpdateTeamRequest request);
    Task<Response.MessageResponse> RemoveMembers(Guid teamId, Request.RemoveMembersRequest request);
    Task<Response.MessageResponse> TransferLeader(Guid teamId, Request.TransferLeaderRequest request);
    Task<Response.RegisterEventResponse> RegisterEvent(Request.RegisterEventRequest request);
    Task<BasePaginationResponse> GetMyRegisteredEvents(Request.GetMyRegisteredEventsRequest request, PaginationRequest paginationRequest);
    Task<Response.RejectionReasonResponse> GetRejectionReason(Guid registerId);
    Task<Response.RegisterEventResponse> ApproveRegistration(Guid registerId);
    Task<Response.RegisterEventResponse> RejectRegistration(Guid registerId, Request.RejectTeamRequest request);
}
