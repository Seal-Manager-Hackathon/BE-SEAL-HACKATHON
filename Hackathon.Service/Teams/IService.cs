using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.Teams;

public interface IService
{
    Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request);
    Task<Response.MessageResponse> InviteMember(Guid teamId, Request.InviteMemberRequest request);
    Task<BasePaginationResponse> GetMyTeams(TeamDetailStatusEnum? status, int pageIndex, int pageSize);
    Task<Response.TeamDetailResponse> GetTeamDetail(Guid teamId);
}
