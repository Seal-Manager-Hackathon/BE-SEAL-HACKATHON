namespace Hackathon.Service.Teams;

public interface IService
{
    Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request);
    Task<Response.InvitationResponse> InviteMember(Guid teamId, Request.InviteMemberRequest request);
    Task<Response.InvitationResponse> RespondInvitation(Guid invitationId, Request.RespondInvitationRequest request);
    Task<Response.TeamResponse> GetTeam(Guid teamId);
    Task<Response.TeamResponse> UpdateTeam(Guid teamId, Request.UpdateTeamRequest request);
    Task<Response.MessageResponse> DeleteMember(Guid teamId, Guid userId);
}
