using Hackathon.Service.Models;

namespace Hackathon.Service.Mentors;

public interface IService
{
    Task<BasePaginationResponse> GetMentorEvents(Request.GetMentorEventsRequest request);
}
