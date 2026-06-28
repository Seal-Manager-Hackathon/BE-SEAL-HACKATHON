using System;
using System.Collections.Generic;
using Hackathon.Repository.Enum;

namespace Hackathon.Service.AssignEvents.Response;

public class AssignLecturerDetailResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid? EventRoleId { get; set; }
    public EventRoleEnum? EventRole { get; set; }
    public RoleEnum Role { get; set; }
    public bool IsDisable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<AssignedTrackInfo> AssignedTracks { get; set; } = new();
}

public class AssignedTrackInfo
{
    public Guid AssignTrackId { get; set; }
    public Guid TrackId { get; set; }
    public string TrackTitle { get; set; } = string.Empty;
    public bool IsDisable { get; set; }
}
