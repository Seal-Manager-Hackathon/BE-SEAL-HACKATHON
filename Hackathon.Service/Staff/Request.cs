using System;
using Hackathon.Service.Models;

namespace Hackathon.Service.Staff;

public static class Request
{
    public class SearchStaffEventsRequest : PaginationRequest
    {
        public string? Keyword { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
    }
}
