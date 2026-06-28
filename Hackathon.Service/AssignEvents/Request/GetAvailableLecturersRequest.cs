using Hackathon.Service.Models;

namespace Hackathon.Service.AssignEvents.Request;

public class GetAvailableLecturersRequest : PaginationRequest
{
    public string? Keyword { get; set; }
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
}
