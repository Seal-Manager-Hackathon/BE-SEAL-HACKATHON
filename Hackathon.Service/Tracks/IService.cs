namespace Hackathon.Service.Tracks;

public interface IService
{
    Task<(List<Response.TrackResponse> Items, int TotalCount)> GetTracks(Guid? eventId, string? keyword, bool? isDisable, int pageIndex, int pageSize);
}
