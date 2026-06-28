using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Service.AssignTracks;

public interface IService
{
    Task<Response.AssignTrackResponse> AssignLecturerToTrack(Guid eventId, Guid trackId, Request.AssignJudgeRequest request);
    Task<List<Response.AssignTrackLecturerResponse>> GetLecturersAssignedToTrack(Guid eventId, Guid trackId, bool? isDisable);
    Task<Guid> RemoveLecturerFromTrack(Guid assignTrackId);
}
