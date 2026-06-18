namespace Hackathon.Service.RegisterTeams;

public static class Request
{
    public class RejectRegisterTeamRequest
    {
        public required string Reason { get; set; }
    }
}
