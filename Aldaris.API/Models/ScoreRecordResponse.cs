namespace Aldaris.API.Controllers;

public class ScoreRecordResponse
{
    public ScoreRecordResponse(string userName, int score, bool isToptaler)
    {
        UserName = userName;
        Score = score;
        IsToptaler = isToptaler;
    }

    public string UserName { get; set; }
    public int Score { get; set; }
    public bool IsToptaler { get; set; }
}