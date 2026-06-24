using Hackathon.Service.AssignTracks.Request;
using Hackathon.Service.AssignTracks.Response;

namespace Hackathon.Service.AssignTracks;

public interface IService
{
    Task<AssignTrackResponse> AssignJudgeToTrack(Guid trackId, AssignJudgeRequest request);
}
