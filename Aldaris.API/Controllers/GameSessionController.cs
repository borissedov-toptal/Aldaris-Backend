using System.ComponentModel.DataAnnotations;
using Aldaris.API.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Aldaris.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameSessionController : ControllerBase
{
    private readonly ILogger<GameSessionController> _logger;
    private readonly Random _random;
    private readonly Mapper _mapper;

    public GameSessionController(
        ILogger<GameSessionController> logger,
        Mapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _random = Random.Shared;
    }

    [HttpPost("StartGame")]
    public GameSessionResponse Create([Required] string userName)
    {
        var session = new GameSession();
        session.UserName = userName;

        _logger.LogDebug($"New session: {session.Id}");

        var response = _mapper.Map<GameSessionResponse>(session);
        return response;
    }

    [HttpGet("{sessionId}")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public GameSessionResponse Get(Guid sessionId)
    {
        var session = new GameSession(sessionId);

        var response = _mapper.Map<GameSessionResponse>(session);
        return response;
    }

    [HttpGet("{sessionId}/GetAnswers")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public IEnumerable<QuestionResponse> GetAnswers(Guid sessionId)
    {
        var session = new GameSession(sessionId);

        for (int i = 0; i < 5; i++)
        {
            var question = new QuestionResponse()
            {
                Id = _random.Next(100),
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                AnswerOptions = new List<QuestionResponse.AnswerOption>()
                {
                    new()
                    {
                        AnswerId = _random.Next(20),
                        Text = "Yes"
                    },
                    new()
                    {
                        AnswerId = _random.Next(20),
                        Text = "No"
                    },
                    new()
                    {
                        AnswerId = _random.Next(20),
                        Text = "I don't know"
                    },
                    new()
                    {
                        AnswerId = _random.Next(20),
                        Text = "Probably"
                    },
                    new()
                    {
                        AnswerId = _random.Next(20),
                        Text = "Probably not"
                    },
                }
            };
            question.AnswerId = question.AnswerOptions[_random.Next(5)].AnswerId;

            yield return question;
        }
    }

    [HttpPost("{sessionId}/SaveAnswer")]
    public GameSessionResponse SaveAnswer(Guid sessionId, int questionId, int answerId)
    {
        var session = new GameSession(sessionId);

        session.GameStage = answerId switch
        {
            10 => GameStage.Suggesting,
            13 => GameStage.UnableToSuggest,
            _ => GameStage.InProgress
        };

        if (session.GameStage == GameStage.Suggesting)
        {
            session.Solution = "COBOL on Wheelchair";
        }

        return _mapper.Map<GameSessionResponse>(session);
    }

    [HttpGet("{sessionId}/NextQuestion")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public QuestionResponse NextQuestion(Guid sessionId)
    {
        var question = new QuestionResponse()
        {
            Id = _random.Next(100),
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            AnswerOptions = new List<QuestionResponse.AnswerOption>()
            {
                new()
                {
                    AnswerId = _random.Next(20),
                    Text = "Yes"
                },
                new()
                {
                    AnswerId = _random.Next(20),
                    Text = "No"
                },
                new()
                {
                    AnswerId = _random.Next(20),
                    Text = "I don't know"
                },
                new()
                {
                    AnswerId = _random.Next(20),
                    Text = "Probably"
                },
                new()
                {
                    AnswerId = _random.Next(20),
                    Text = "Probably not"
                },
            }
        };
        return question;
    }

    [HttpPost("{sessionId}/AcceptSolution")]
    public GameSessionResponse AcceptSolution(Guid sessionId)
    {
        var session = new GameSession();
        session.GameStage = GameStage.Completed;

        return _mapper.Map<GameSessionResponse>(session);
    }

    [HttpPost("{sessionId}/SuggestSolution")]
    public GameSessionResponse SuggestSolution(Guid sessionId, [Required] string solution)
    {
        var session = new GameSession(sessionId);
        session.Solution = solution;
        session.GameStage = GameStage.ResolvePending;

        return _mapper.Map<GameSessionResponse>(session);
    }
}