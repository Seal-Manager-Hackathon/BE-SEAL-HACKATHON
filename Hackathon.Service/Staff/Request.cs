using System;
using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.Staff;

public static class Request
{
    public class SearchStaffEventsRequest : PaginationRequest
    {
        public string? Keyword { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public EventStatusEnum? Status { get; set; }
    }
}
