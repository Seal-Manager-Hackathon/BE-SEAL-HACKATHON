namespace Hackathon.Service.Teams;

public interface IService
{
    Task<Response.CreateTeamResponse> CreateTeam(Request.CreateTeamRequest request);
}
