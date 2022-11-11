using Aldaris.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aldaris.API.Data;

public class AldarisContext : DbContext
{
    public AldarisContext(DbContextOptions<AldarisContext> options) : base(options)
    {
    }

    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameSession>()
            .HasMany(p => p.Questions)
            .WithMany(p => p.GameSessions)
            .UsingEntity<GameSessionAnswer>(
                j => j
                    .HasOne(pt => pt.Question)
                    .WithMany(t => t.GameSessionAnswers)
                    .HasForeignKey(pt => pt.QuestionId),
                j => j
                    .HasOne(pt => pt.GameSession)
                    .WithMany(p => p.GameSessionAnswers)
                    .HasForeignKey(pt => pt.GameSessionId),
                j =>
                {
                    j.Property(pt => pt.AskedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.GameSessionId, t.QuestionId });
                });

        modelBuilder.Entity<GameSession>()
            .HasMany(p => p.Answers)
            .WithMany(p => p.GameSessions)
            .UsingEntity<GameSessionAnswer>(
                j => j
                    .HasOne(pt => pt.Answer)
                    .WithMany(t => t.GameSessionAnswers)
                    .HasForeignKey(pt => pt.AnswerId),
                j => j
                    .HasOne(pt => pt.GameSession)
                    .WithMany(p => p.GameSessionAnswers)
                    .HasForeignKey(pt => pt.GameSessionId),
                j => { });
        
        modelBuilder.Entity<Question>()
            .HasMany(p => p.PossibleAnswers)
            .WithMany(p => p.ParentQuestions)
            .UsingEntity<QuestionPossibleAnswer>(
                j => j
                    .HasOne(pt => pt.Answer)
                    .WithMany(t => t.QuestionPossibleAnswers)
                    .HasForeignKey(pt => pt.AnswerId),
                j => j
                    .HasOne(pt => pt.Question)
                    .WithMany(p => p.QuestionPossibleAnswers)
                    .HasForeignKey(pt => pt.QuestionId),
                j =>
                {
                    j.HasKey(t => new { t.QuestionId, t.AnswerId });
                });
    }
}