namespace Aldaris.API.Domain;

public class QuestionPossibleAnswer
{
    public int QuestionId { get; set; }

    public Question Question { get; set; }
    

    public int AnswerId { get; set; }

    public Answer Answer { get; set; }
}