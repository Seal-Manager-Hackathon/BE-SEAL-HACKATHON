using Hackathon.Service.Models;

namespace Hackathon.Service.Tracks;

public interface IService
{
    Task<BasePaginationResponse> GetTracks(Guid? eventId, string? keyword, bool? isDisable, PaginationRequest paginationRequest);
}
