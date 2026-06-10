using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class LeaderBoards : BaseEntity<Guid>, IAuditableEntity
{
    public Guid EventId { get; set; }
    public int? Year { get; set; }

    public Events Event { get; set; } = null!;
    public ICollection<LeaderBoardDetails> LeaderBoardDetails { get; set; } = new List<LeaderBoardDetails>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}