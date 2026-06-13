namespace Hackathon.Service.LeaderBoards;

public static class Response
{
    public class LeaderBoardItemResponse
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public decimal TotalScore { get; set; }
        public int Rank { get; set; }
    }
}
