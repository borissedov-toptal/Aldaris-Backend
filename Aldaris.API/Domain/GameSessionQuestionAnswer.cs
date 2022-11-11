namespace Aldaris.API.Domain;

public class GameSessionAnswer
{
    public int GameSessionId { get; set; }
    
    public GameSession GameSession { get; set; }
    
    
    public int QuestionId { get; set; }

    public Question Question { get; set; }
    
    public DateTime AskedAt { get; set; }


    public int? AnswerId { get; set; }

    public Answer Answer { get; set; }
    
    public DateTime? AnsweredAt { get; set; }

}