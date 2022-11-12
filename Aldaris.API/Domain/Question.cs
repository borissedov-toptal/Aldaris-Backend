namespace Aldaris.API.Domain;

public class Question
{
    public Question(string text, string variableName)
    {
        Text = text;
        VariableName = variableName;
    }

    public int Id { get; set; }

    public string Text { get; set; }

    public ICollection<Answer> PossibleAnswers { get; set; } = new List<Answer>();
    public List<QuestionPossibleAnswer> QuestionPossibleAnswers { get; set; } = new();

    public ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
    public List<GameSessionAnswer> GameSessionAnswers { get; set; } = new();

    public string VariableName { get; set; }

}