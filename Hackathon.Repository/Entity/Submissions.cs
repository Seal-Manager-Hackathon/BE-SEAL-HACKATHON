using Hackathon.Repository.Abtraction;
using Hackathon.Repository.Enum;

namespace Hackathon.Repository.Entity;

public class Submissions : BaseEntity<Guid>, IAuditableEntity
{
    public Guid RoundDetailId { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public SubmissionStatusEnum? Status { get; set; }
    public DateTimeOffset? SubmittedAt { get; set; }
    public bool IsRegrade { get; set; }

    public RoundDetails RoundDetail { get; set; } = null!;
    public ICollection<Scores> Scores { get; set; } = new List<Scores>();
    public Reports? Report { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}