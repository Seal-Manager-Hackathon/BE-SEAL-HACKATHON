namespace Hackathon.Service.RegisterTeams;

public static class Request
{
    public class RegisterEventRequest
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "TEAM_ID_REQUIRED")]
        public Guid TeamId { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "EVENT_ID_REQUIRED")]
        public Guid EventId { get; set; }

        public string? Description { get; set; }
    }

    public class GetMyRegisteredEventsRequest
    {
        public string? Status { get; set; }
    }

    public class GetTeamRegisteredEventsRequest
    {
        public string? Status { get; set; }
    }

    public class RejectRegisterTeamRequest
    {
        public required string Reason { get; set; }
    }

    public class BanTeamRequest
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "REASON_IS_REQUIRED")]
        public required string Reason { get; set; }
    }
}
