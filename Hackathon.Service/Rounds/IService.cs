namespace Hackathon.Service.Rounds;

public interface IService
{
    Task<List<Response.RoundResponse>> GetRounds(Guid eventId);
    Task<List<Response.MyRoundResponse>> GetMyRounds(Guid? eventId);
    Task<Response.SubmitAssignmentResponse> SubmitAssignment(Guid roundId, Request.SubmitAssignmentRequest request);
}
