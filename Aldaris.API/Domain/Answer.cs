namespace Aldaris.API.Domain;

public class Answer
{
    public Answer(string text, string clauseType, string clauseValue)
    {
        Text = text;
        ClauseType = clauseType;
        ClauseValue = clauseValue;
    }

    public int Id { get; set; }

    public string Text { get; set; }

    public ICollection<Question> ParentQuestions { get; set; } = new List<Question>();
    public List<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; } = new();

    public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
    public List<GameSessionAnswer> GameSessionAnswers { get; set; } = new();

    public string ClauseType { get; set; }

    public string ClauseValue { get; set; }
}