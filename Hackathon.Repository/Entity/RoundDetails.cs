using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class RoundDetails : BaseEntity<Guid>, IAuditableEntity
{
    public Guid RoundId { get; set; }
    public Guid RegisterTeamId { get; set; }

    public Rounds Round { get; set; } = null!;
    public RegisterTeams RegisterTeam { get; set; } = null!;
    public ICollection<Submissions> Submissions { get; set; } = new List<Submissions>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}