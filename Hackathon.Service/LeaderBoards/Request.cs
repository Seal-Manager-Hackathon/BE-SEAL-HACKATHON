namespace Hackathon.Service.LeaderBoards;

public static class Request
{
    public class AssignAwardRequest
    {
        public decimal? Score { get; set; }
        public string? LevelAward { get; set; }
    }
}
