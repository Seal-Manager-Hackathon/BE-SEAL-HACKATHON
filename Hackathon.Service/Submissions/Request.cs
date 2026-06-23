using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Submissions;

public static class Request
{
    public class SubmitRoundProjectRequest
    {
        [Required(ErrorMessage = "URL_REQUIRED")]
        [Url(ErrorMessage = "INVALID_URL_FORMAT")]
        public required string Url { get; set; }

        public string? Description { get; set; }
    }

    public class GetSubmissionsRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO")]
        public int PageIndex { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "PAGE_SIZE_MUST_BE_GREATER_THAN_ZERO")]
        public int PageSize { get; set; } = 10;
    }
}
