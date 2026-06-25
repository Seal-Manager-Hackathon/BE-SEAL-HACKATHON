using System.Collections.Generic;
using System.Threading.Tasks;
using Hackathon.Service.Models;

namespace Hackathon.Service.Staff;

public interface IService
{
    Task<List<Response.StaffEventResponse>> GetCurrentStaffEvents();
    Task<BasePaginationResponse> GetStaffEvents(PaginationRequest request);
    Task<BasePaginationResponse> SearchStaffEvents(Request.SearchStaffEventsRequest request);
}
