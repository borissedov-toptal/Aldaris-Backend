using System.ComponentModel.DataAnnotations;
using Aldaris.API.Data;
using Aldaris.API.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aldaris.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameSessionController : ControllerBase
{
    private readonly ILogger<GameSessionController> _logger;
    private readonly Random _random;
    private readonly Mapper _mapper;
    private readonly AldarisContext _context;

    public GameSessionController(
        ILogger<GameSessionController> logger,
        Mapper mapper,
        AldarisContext context
    )
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _random = Random.Shared;
    }

    [HttpPost("StartGame")]
    public ActionResult<GameSessionResponse> Create([Required] string userName)
    {
        var session = new GameSession(userName);
        _context.GameSessions.Add(session);
        _context.SaveChanges();
        
        var response = _mapper.Map<GameSessionResponse>(session);
        return response;
    }

    [HttpGet("{sessionId}")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public ActionResult<GameSessionResponse> Get(Guid sessionId)
    {
        var session = _context.GameSessions.Find(sessionId);

        if (session == null)
        {
            return NotFound();
        }

        var response = _mapper.Map<GameSessionResponse>(session);
        return response;
    }

    [HttpGet("{sessionId}/GetAnswers")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public ActionResult<IEnumerable<QuestionResponse>> GetAnswers(Guid sessionId)
    {
        var session = _context.GameSessions
            .Include(s => s.GameSessionAnswers)
            .ThenInclude(s => s.Question)
            .ThenInclude(q => q.PossibleAnswers)
            .Include(s => s.GameSessionAnswers)
            .ThenInclude(s => s.Answer)
            .AsNoTracking()
            .FirstOrDefault(s => s.Id == sessionId);

        if (session == null)
        {
            return NotFound();
        }

        var response = new List<QuestionResponse>();
        foreach (var sessionAnswer in session.GameSessionAnswers.OrderBy(gsa => gsa.AnsweredAt))
        {
            var questionResponse = _mapper.Map<QuestionResponse>(sessionAnswer.Question);
            questionResponse.AnswerOptions =
                sessionAnswer.Question.PossibleAnswers.Select(_mapper.Map<QuestionResponse.AnswerOption>).ToArray();
            questionResponse.AskedAt = sessionAnswer.AskedAt;
            questionResponse.AnswerId = sessionAnswer.AnswerId;
            questionResponse.AnsweredAt = sessionAnswer.AnsweredAt;
            response.Add(questionResponse);
        }

        return response;
    }

    [HttpPost("{sessionId}/SaveAnswer")]
    public ActionResult<GameSessionResponse> SaveAnswer(Guid sessionId, int questionId, int answerId, GameStage? forceGameSessionStage = null)
    {
        var session = _context.GameSessions
            .Include(s => s.GameSessionAnswers)
            .ThenInclude(s => s.Question)
            .ThenInclude(q => q.PossibleAnswers)
            .FirstOrDefault(s => s.Id == sessionId);
        
        if (session == null)
        {
            return NotFound();
        }

        var existingQuestion = session.GameSessionAnswers.FirstOrDefault(sq => sq.QuestionId == questionId);
        if (existingQuestion != null)
        {
            existingQuestion.AnswerId = answerId;
            existingQuestion.AnsweredAt = DateTime.Now;
        }
        else
        {
            session.GameSessionAnswers.Add(new GameSessionAnswer
            {
                QuestionId = questionId,
                AskedAt = DateTime.Now,
                AnswerId = answerId,
                AnsweredAt = DateTime.Now
            });
        }
        
        //To be replaced after debugging
        if (forceGameSessionStage != null)
        {
            session.GameStage = forceGameSessionStage.Value;
        }
        
        if (session.GameStage == GameStage.Suggesting)
        {
            session.Solution = "COBOL on Wheelchair";
        }

        _context.SaveChanges();

        return _mapper.Map<GameSessionResponse>(session);
    }

    [HttpGet("{sessionId}/NextQuestion")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public ActionResult<QuestionResponse> NextQuestion(Guid sessionId)
    {
        var session = _context.GameSessions
            .Include(s => s.GameSessionAnswers)
            .ThenInclude(s => s.Question)
            .FirstOrDefault(s => s.Id == sessionId);

        if (session == null)
        {
            return NotFound();
        }

        //To be replaced after debugging
        var question = _context.Questions.OrderBy(r => Guid.NewGuid()).First();
        
        //ASSIGN QUESTION TO CURRENT SESSION
        
        var questionResponse = _mapper.Map<QuestionResponse>(question);
        questionResponse.AnswerOptions =
            question.PossibleAnswers.Select(_mapper.Map<QuestionResponse.AnswerOption>).ToArray();
        
        return questionResponse;
    }

    [HttpPost("{sessionId}/AcceptSolution")]
    public ActionResult<GameSessionResponse> AcceptSolution(Guid sessionId)
    {
        var session = _context.GameSessions.Find(sessionId);

        if (session == null)
        {
            return NotFound();
        }

        session.GameStage = GameStage.Completed;

        _context.SaveChanges();

        return _mapper.Map<GameSessionResponse>(session);
    }

    [HttpPost("{sessionId}/SuggestSolution")]
    public ActionResult<GameSessionResponse> SuggestSolution(Guid sessionId, [Required] string solution)
    {
        var session = _context.GameSessions.Find(sessionId);

        if (session == null)
        {
            return NotFound();
        }

        session.Solution = solution;
        session.GameStage = GameStage.ResolvePending;

        _context.SaveChanges();

        return _mapper.Map<GameSessionResponse>(session);
    }
}