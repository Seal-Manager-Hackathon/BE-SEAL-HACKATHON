namespace Hackathon.Service.Topics;

public interface IService
{
    Task<Response.AssignedTopicResponse> GetTopic(Guid eventId, Guid registerTeamId);
}
