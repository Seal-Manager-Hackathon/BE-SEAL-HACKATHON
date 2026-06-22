namespace Hackathon.Service.Teams;

public static class Request
{
    public class CreateTeamRequest
    {
        public string? TeamName { get; set; }
    }

    public class InviteMemberRequest
    {
        public string? Email { get; set; }

        public string? Description { get; set; }
    }

    public class UpdateTeamRequest
    {
        public string? TeamName { get; set; }
    }

    public class RemoveMembersRequest
    {
        public List<Guid> UserIds { get; set; } = new();
    }

    public class TransferLeaderRequest
    {
        public Guid NewLeaderId { get; set; }
    }

    public class RegisterEventRequest
    {
        public Guid TeamId { get; set; }

        public Guid EventId { get; set; }

        public string? Description { get; set; }
    }

    public class GetMyRegisteredEventsRequest
    {
        public string? Status { get; set; }
    }
}
