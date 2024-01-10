using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Models
{
    public class QuizDataBaseContext:DbContext
    {
        protected readonly IConfiguration Configuration;

        public QuizDataBaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(Configuration.GetConnectionString("QuizDataBase"));

        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameResult> GamesResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameResult>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.TeamId}).HasName("game_result_pkey");
            });
        }
    }
}
