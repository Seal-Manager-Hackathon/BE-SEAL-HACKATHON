using Hackathon.Repository.Enum;
using Hackathon.Service.Models;

namespace Hackathon.Service.Admin.Request;

public class GetUsersQuery
{
    public string? MailSearch { get; set; }
    public Guid? IdSearch { get; set; }
    public RoleEnum? Role { get; set; }
    public string? StudentIdSearch { get; set; }
    public bool? IsDisable { get; set; }
    public bool? IsVerified { get; set; }
    public PaginationRequest Pagination { get; set; } = new();
}
