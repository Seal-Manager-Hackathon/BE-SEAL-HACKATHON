using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.RegisterTeams;

public static class Request
{
    public class RegisterEventRequest
    {
        [Required(ErrorMessage = "TEAM_ID_REQUIRED")]
        public Guid TeamId { get; set; }

        [Required(ErrorMessage = "EVENT_ID_REQUIRED")]
        public Guid EventId { get; set; }

        public string? Description { get; set; }
    }

    public class GetMyRegisteredEventsRequest
    {
        public string? Status { get; set; }
    }
}
