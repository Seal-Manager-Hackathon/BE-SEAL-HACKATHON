using Hackathon.Service.AssignEvents.Request;
using Hackathon.Service.AssignEvents.Response;
using Hackathon.Service.Models;

namespace Hackathon.Service.AssignEvents;

public interface IService
{
    Task<AssignEventResponse> AssignLecturerToEvent(Guid eventId, AssignLecturerRequest request);
    Task<BasePaginationResponse> GetAssignedLecturersByEvent(Guid eventId, Guid? eventRoleId, string? keyword, bool? isDisable, PaginationRequest paginationRequest);
    Task<Guid> RemoveLecturerAssignment(Guid assignEventId);
}
