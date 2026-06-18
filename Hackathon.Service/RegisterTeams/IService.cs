using Hackathon.Service.Models;

namespace Hackathon.Service.RegisterTeams;

public interface IService
{
    Task<Response.RegisterEventResponse> RegisterEvent(Request.RegisterEventRequest request);
    Task<BasePaginationResponse> GetMyRegisteredEvents(Request.GetMyRegisteredEventsRequest request, PaginationRequest paginationRequest);
    Task<Response.RejectionReasonResponse> GetRejectionReason(Guid registerId);
}
