namespace Hackathon.Repository.Abtraction;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
    public bool IsDisable { get; set; }
}