using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.RegisterTeams;

public interface IService
{
    Task<BasePaginationResponse> GetRegisterTeamsByEvent(Guid eventId, string? keyword, RegisterTeamStatusEnum? status, bool? isDisable, PaginationRequest paginationRequest);
    Task<Response.RegisterTeamDetailResponse> GetRegisterTeamDetail(Guid registerTeamId);
    Task<Response.RegisterTeamActionResponse> AcceptRegisterTeam(Guid registerTeamId);
    Task<Response.RegisterTeamActionResponse> RejectRegisterTeam(Guid registerTeamId, Request.RejectRegisterTeamRequest request);
}
