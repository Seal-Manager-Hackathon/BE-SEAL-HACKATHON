using System;
using System.Threading.Tasks;
using Hackathon.Repository.Enum;
using Hackathon.Service.AssignEvents.Request;
using Hackathon.Service.AssignEvents.Response;
using Hackathon.Service.Models;

namespace Hackathon.Service.AssignEvents;

public interface IService
{
    Task<AssignEventResponse> AssignLecturerToEvent(Guid eventId, AssignLecturerRequest request);
    Task<BasePaginationResponse> GetAvailableLecturers(Guid eventId, GetAvailableLecturersRequest request);
    Task<BasePaginationResponse> GetEventAssignments(Guid eventId, EventRoleEnum? eventRole, string? keyword, Guid? trackId, bool? isDisable, PaginationRequest paginationRequest);
    Task<Guid> RemoveLecturerAssignment(Guid assignEventId);
}
