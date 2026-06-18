using System.ComponentModel.DataAnnotations;
using Hackathon.Service.Models;

namespace Hackathon.Service.Events;

public static class Request
{
    public class GetEventsRequest : PaginationRequest
    {
        public string? Keyword { get; set; }
        public int? Year { get; set; }
        public string? Status { get; set; }
    }

    public class GetEventsForAdminRequest : PaginationRequest
    {
        public string? Keyword { get; set; }
        public int? Year { get; set; }
        public string? Status { get; set; }
        public bool? IsDisable { get; set; }
    }

    public class GetJoinedEventsRequest : PaginationRequest
    {
        public string? Keyword { get; set; }
        public int? Year { get; set; }
        public string? Status { get; set; }
    }
}
