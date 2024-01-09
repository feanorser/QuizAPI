namespace QuizAPI.Models
{
    public class Games
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }
        public string? GameType {  get; set; }
        public int Rounds { get; set; }
    }
}
