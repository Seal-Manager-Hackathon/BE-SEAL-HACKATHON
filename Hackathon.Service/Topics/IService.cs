namespace Hackathon.Service.Topics;

public interface IService
{
    Task<Response.AssignedTopicResponse> GetTopic(Guid eventId, Guid registerTeamId);
    Task<Response.TopicDetailResponse> GetTopicDetail(Guid topicId);
    Task<Response.CreateTopicResponse> CreateTopic(Guid trackId, Request.CreateTopicRequest request);
    Task<string> UpdateTopic(Guid topicId, Request.UpdateTopicRequest request);
    Task<string> DeleteTopic(Guid topicId);
}
