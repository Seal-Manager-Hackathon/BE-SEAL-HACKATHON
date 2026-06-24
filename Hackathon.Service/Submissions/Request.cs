namespace Hackathon.Service.Submissions;

public static class Request
{
    public class SubmitRoundProjectRequest
    {
        public required string Url { get; set; }

        public string? Description { get; set; }
    }

    public class GetSubmissionsRequest
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
