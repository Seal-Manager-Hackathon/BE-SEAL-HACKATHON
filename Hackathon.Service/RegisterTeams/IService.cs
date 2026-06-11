namespace Hackathon.Service.RegisterTeams;

public interface IService
{
    Task<Response.RegisterTeamResponse> RegisterTeamForEvent(Request.RegisterTeamRequest request);
    Task<Response.RegisterTeamResponse> GetMyRegistrationStatus(Guid registerTeamId);
    Task<List<Response.AssignedEventResponse>> GetAssignedEvents();
    Task<List<Response.PendingRegisterTeamResponse>> GetPendingTeamsByEvent(Guid eventId);
    Task<Response.RegisterTeamDetailResponse> GetRegistrationDetailForReview(Guid registerTeamId);
    Task<Response.RegisterTeamResponse> ApproveRegistration(Guid registerTeamId);
    Task<Response.RegisterTeamResponse> RejectRegistration(Guid registerTeamId, Request.RejectRegistrationRequest request);
}
