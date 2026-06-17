using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Teams;

public static class Request
{
    public class CreateTeamRequest
    {
        [Required(ErrorMessage = "TEAM_NAME_REQUIRED")]
        public string? TeamName { get; set; }
    }
}
