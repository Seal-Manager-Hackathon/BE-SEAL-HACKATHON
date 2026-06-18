using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Teams;

public static class Request
{
    public class CreateTeamRequest
    {
        [Required(ErrorMessage = "TEAM_NAME_REQUIRED")]
        public string? TeamName { get; set; }
    }

    public class InviteMemberRequest
    {
        [Required(ErrorMessage = "EMAIL_REQUIRED")]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL_FORMAT")]
        public string? Email { get; set; }

        public string? Description { get; set; }
    }

    public class UpdateTeamRequest
    {
        [Required(ErrorMessage = "TEAM_NAME_REQUIRED")]
        public string? TeamName { get; set; }
    }

    public class RemoveMembersRequest
    {
        [Required(ErrorMessage = "USER_IDS_REQUIRED")]
        public List<Guid> UserIds { get; set; } = new();
    }

    public class TransferLeaderRequest
    {
        [Required(ErrorMessage = "NEW_LEADER_ID_REQUIRED")]
        public Guid NewLeaderId { get; set; }
    }

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

    public class RejectTeamRequest
    {
        [Required(ErrorMessage = "REJECTION_REASON_REQUIRED")]
        public string? Reason { get; set; }
    }
}
