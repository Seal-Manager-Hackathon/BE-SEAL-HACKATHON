using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hackathon.Service.AssignTracks.Request;
using Hackathon.Service.AssignTracks.Response;

namespace Hackathon.Service.AssignTracks;

public interface IService
{
    Task<AssignTrackResponse> AssignJudgeToTrack(Guid trackId, AssignJudgeRequest request);
    Task<List<AssignTrackLecturerResponse>> GetLecturersAssignedToTrack(Guid eventId, Guid trackId, bool? isDisable);
}
