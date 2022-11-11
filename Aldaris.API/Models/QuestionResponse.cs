namespace Aldaris.API.Domain;

public class QuestionResponse
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public DateTime AskedAt { get; set; }

    public IList<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
    
    public int? AnswerId { get; set; }
    
    public DateTime? AnsweredAt { get; set; }

    public class AnswerOption
    {
        public int Id { get; set; }
        public string? Text { get; set; }
    }
}