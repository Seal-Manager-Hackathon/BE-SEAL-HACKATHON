namespace Hackathon.Service.Submissions;

public interface IService
{
    Task<Response.SubmissionDetailResponse> GetSubmissionDetail(Guid submissionId);
}
