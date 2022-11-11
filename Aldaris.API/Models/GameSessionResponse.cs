namespace Aldaris.API.Domain;

public class GameSessionResponse
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserName { get; set; }

    public GameStage GameStage { get; set; }

    public string? Solution { get; set; }

    public GameSessionResponse()
    {
        Id = Guid.NewGuid();
        GameStage = GameStage.NewGame;
        CreatedAt = DateTime.Now;
    }

    public GameSessionResponse(Guid id)
    {
        Id = id;
        GameStage = GameStage.NewGame;
        CreatedAt = DateTime.Now;
    }
}