namespace Aldaris.API.Domain;

public class GameSession
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserName { get; set; }

    public GameStage GameStage { get; set; }

    public string? Solution { get; set; }
    
    public ICollection<Question> Questions { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public List<GameSessionAnswer> GameSessionAnswers { get; set; }
    
    
    public GameSession(string userName)
    {
        UserName = userName;
        GameStage = GameStage.NewGame;
        CreatedAt = DateTime.Now;
    }
}