namespace QuizAPI.Models
{
    public class GameResult
    {
        public Guid GameId { get; set; }
        public Guid TeamId { get; set; }
        public Team? Team { get; set; }
        public Game? Game { get; set; }
        public float[]? PointsInRounds { get; set; }
    }
}
