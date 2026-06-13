namespace Hackathon.Service.Teams;

public static class Request
{
    public class CreateTeamRequest
    {
        public string Name { get; set; } = null!;
    }

    public class InviteMemberRequest
    {
        public Guid UserId { get; set; }
        public string? Description { get; set; }
    }

    public class RespondInvitationRequest
    {
        public bool IsAccepted { get; set; }
    }

    public class UpdateTeamRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
