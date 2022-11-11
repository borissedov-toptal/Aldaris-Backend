namespace Aldaris.API.Domain;

public class Answer
{
    public int Id { get; set; }

    public string Text { get; set; }
    
    public ICollection<Question> ParentQuestions { get; set; }
    public List<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; }

    public ICollection<GameSession> GameSessions { get; set; }
    public List<GameSessionAnswer> GameSessionAnswers { get; set; }
}