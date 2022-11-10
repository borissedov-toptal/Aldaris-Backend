using System.ComponentModel.DataAnnotations;
using Aldaris.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Aldaris.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameSessionController : ControllerBase
{
    private readonly ILogger<GameSessionController> _logger;
    private readonly Random _random;

    public GameSessionController(ILogger<GameSessionController> logger)
    {
        _logger = logger;
        _random = Random.Shared;
    }

    [HttpPost("StartGame")]
    public GameSession Create(string userName)
    {
        var session = new GameSession();
        session.UserName = userName;

        _logger.LogDebug($"New session: {session.Id}");

        return new GameSession();
    }

    [HttpGet("{sessionId}")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public GameSession Get(Guid sessionId)
    {
        var session = new GameSession(sessionId);

        return session;
    }
    
    [HttpPost("{sessionId}/SaveAnswer")]
    public GameSession SaveAnswer(Guid sessionId, int questionId, int answerId)
    {
        var session = new GameSession(sessionId);

        session.GameStage = answerId switch
        {
            10 => GameStage.Suggesting,
            13 => GameStage.UnableToSuggest,
            _ => GameStage.InProgress
        };
        
        if(session.GameStage == GameStage.Suggesting)
        {
            session.Solution = "COBOL on Wheelchair";
        }

        return session;
    }

    [HttpGet("{sessionId}/NextQuestion")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public Question NextQuestion(Guid sessionId)
    {
        var question = new Question
        {
            Id = _random.Next(100),
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            Answers = new List<Answer>()
            {
                new()
                {
                    Id = _random.Next(20),
                    Text = "Yes"
                },
                new()
                {
                    Id = _random.Next(20),
                    Text = "No"
                },
                new()
                {
                    Id = _random.Next(20),
                    Text = "I don't know"
                },
                new()
                {
                    Id = _random.Next(20),
                    Text = "Probably"
                },
                new()
                {
                    Id = _random.Next(20),
                    Text = "Probably not"
                },
            }
        };
        return question;
    }

    [HttpPost("{sessionId}/AcceptSolution")]
    public GameSession AcceptSolution(Guid sessionId)
    {
        var session = new GameSession();
        session.GameStage = GameStage.Completed;

        return session;
    }

    [HttpPost("{sessionId}/SuggestSolution")]
    public GameSession SuggestSolution(Guid sessionId, [Required] string solution)
    {
        var session = new GameSession(sessionId);
        session.Solution = solution;
        session.GameStage = GameStage.ResolvePending;

        return session;
    }
}