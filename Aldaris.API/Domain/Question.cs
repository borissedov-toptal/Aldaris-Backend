namespace Aldaris.API.Domain;

public class Question
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public IList<Answer> Answers { get; set; } = new List<Answer>();
}