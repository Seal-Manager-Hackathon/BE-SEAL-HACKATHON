namespace Hackathon.Service.RegisterTeam;

public static class Request
{
    public class RejectRegisterTeamRequest
    {
        public required string Reason { get; set; }
    }
}
