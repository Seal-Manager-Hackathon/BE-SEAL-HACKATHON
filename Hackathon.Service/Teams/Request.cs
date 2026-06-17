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
}
