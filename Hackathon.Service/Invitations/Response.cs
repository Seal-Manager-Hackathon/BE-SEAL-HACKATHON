using System;

namespace Hackathon.Service.Invitations;

public static class Response
{
    public class InvitationItemResponse
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? LimitTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
