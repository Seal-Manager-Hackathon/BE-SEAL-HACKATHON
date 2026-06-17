using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Events;

public static class Request
{
    public class GetEventsRequest
    {
        public string? Keyword { get; set; }
        public int? Year { get; set; }
        public string? Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO")]
        public int PageIndex { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "PAGE_SIZE_MUST_BE_GREATER_THAN_ZERO")]
        public int PageSize { get; set; } = 10;
    }

    public class GetEventsForAdminRequest
    {
        public string? Keyword { get; set; }
        public int? Year { get; set; }
        public string? Status { get; set; }
        public bool? IsDisable { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PAGE_INDEX_MUST_BE_GREATER_THAN_ZERO")]
        public int PageIndex { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "PAGE_SIZE_MUST_BE_GREATER_THAN_ZERO")]
        public int PageSize { get; set; } = 10;
    }
}
