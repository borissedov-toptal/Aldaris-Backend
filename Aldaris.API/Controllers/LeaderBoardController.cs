using Aldaris.API.Data;
using Aldaris.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Aldaris.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderBoardController : ControllerBase
{
    private readonly AldarisContext _context;

    public LeaderBoardController(AldarisContext context)
    {
        _context = context;
    }

    [HttpGet("GetLeadersWithScores")]
    [ResponseCache(Duration = 60)]
    public IEnumerable<ScoreRecordResponse> GetLeadersWithScores(int count = 10)
    {
        return (from p in _context.GameSessions
                group p by
                    p.UserName
                into g
                select new ScoreRecordResponse
                (
                    g.Key,
                    g.Sum(x => x.GameStage == GameStage.Completed
                        ? 20
                        : x.GameStage == GameStage.Resolved
                            ? 50
                            : 0),
                    true
                )
            )
            .ToArray()
            .OrderByDescending(x => x.Score)
            .Take(count)
            .ToArray();
    }
}