using System.ComponentModel.DataAnnotations;

namespace Hackathon.Service.Auth;

public class SecurityOption
{
    [Required]public string Pepper { get; set; } = null!;

}
