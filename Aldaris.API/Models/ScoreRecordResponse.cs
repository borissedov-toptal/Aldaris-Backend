namespace Aldaris.API.Controllers;

public class ScoreRecordResponse
{
    public ScoreRecordResponse(string userName, int score, bool isToptaler, DateTime lastPlayedDate)
    {
        UserName = userName;
        Score = score;
        IsToptaler = isToptaler;
        LastPlayedLastPlayedDate = lastPlayedDate;
    }

    public DateTime LastPlayedLastPlayedDate { get; set; }

    public string UserName { get; set; }
    public int Score { get; set; }
    public bool IsToptaler { get; set; }
}