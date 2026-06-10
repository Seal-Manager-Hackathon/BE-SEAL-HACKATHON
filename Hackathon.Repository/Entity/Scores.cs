using Hackathon.Repository.Abtraction;

namespace Hackathon.Repository.Entity;

public class Scores : BaseEntity<Guid>, IAuditableEntity
{
    public Guid SubmissionId { get; set; }
    public Guid AssignTrackId { get; set; }
    public bool IsRetake { get; set; }
    public decimal? TotalScore { get; set; }
    public bool IsMock { get; set; }

    public Submissions Submission { get; set; } = null!;
    public AssignTracks AssignTrack { get; set; } = null!;
    public ICollection<ScoreItems> ScoreItems { get; set; } = new List<ScoreItems>();

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}