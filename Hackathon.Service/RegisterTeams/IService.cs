using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.RegisterTeams;

public interface IService
{
    Task<Response.RegisterTeamActionResponse> RegisterEvent(Request.RegisterEventRequest request);
    Task<BasePaginationResponse> GetMyRegisteredEvents(Request.GetMyRegisteredEventsRequest request, PaginationRequest paginationRequest);
    Task<Response.RegisterTeamDetailForStudentResponse> GetRegisterTeamDetailForStudent(Guid registerId);

    Task<BasePaginationResponse> GetRegisterTeamsByEvent(Guid eventId, string? keyword, RegisterTeamStatusEnum? status, bool? isDisable, PaginationRequest paginationRequest);
    Task<Response.RegisterTeamDetailResponse> GetRegisterTeamDetail(Guid registerTeamId);
    Task<Response.RegisterTeamActionResponse> AcceptRegisterTeam(Guid registerTeamId);
    Task<Response.RegisterTeamActionResponse> RejectRegisterTeam(Guid registerTeamId, Request.RejectRegisterTeamRequest request);
}
