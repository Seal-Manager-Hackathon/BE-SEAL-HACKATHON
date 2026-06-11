namespace Hackathon.Service.RegisterTeams;

public static class Request
{
    public class RegisterTeamRequest
    {
        public Guid TeamId { get; set; }
        public Guid EventId { get; set; }
        public string? Description { get; set; }
    }

    public class RejectRegistrationRequest
    {
        public string Reason { get; set; } = null!;
    }
}
