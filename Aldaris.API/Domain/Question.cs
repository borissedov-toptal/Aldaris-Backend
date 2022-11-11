namespace Aldaris.API.Domain;

public class Question
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public ICollection<Answer> PossibleAnswers { get; set; }
    public List<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; }
    
    public ICollection<GameSession> GameSessions { get; set; }
    public List<GameSessionAnswer> GameSessionAnswers { get; set; }
}