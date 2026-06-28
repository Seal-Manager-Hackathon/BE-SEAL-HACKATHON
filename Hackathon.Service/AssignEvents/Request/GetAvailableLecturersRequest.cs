using Hackathon.Service.Models;

namespace Hackathon.Service.AssignEvents.Request;

public class GetAvailableLecturersRequest : PaginationRequest
{
    public Guid EventRoleId { get; set; }
    public string? Keyword { get; set; }
}
