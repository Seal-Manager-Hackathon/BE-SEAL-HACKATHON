using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Rounds;

public static class Request
{
    public class SubmitAssignmentRequest
    {
        public string? Url { get; set; }
        public string? Description { get; set; }
    }
}
