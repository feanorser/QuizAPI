namespace QuizAPI.Models
{
    public class GamesResults
    {
        public Guid IdGame { get; set; }
        public Guid IdTeam { get; set; }
        public int[] PointsInRounds { get; set; }
    }
}
