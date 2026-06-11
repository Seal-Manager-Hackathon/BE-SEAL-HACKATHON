namespace Hackathon.Service.Teams;

public interface IService
{
    Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request);
    Task<Response.InvitationResponse> InviteMember(Guid teamId, Request.InviteMemberRequest request);
    Task<Response.InvitationResponse> RespondInvitation(Guid invitationId, Request.RespondInvitationRequest request);
}
