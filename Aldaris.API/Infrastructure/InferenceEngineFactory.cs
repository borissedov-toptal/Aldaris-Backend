using Aldaris.API.Data;
using Aldaris.ExpertSystem;
using Microsoft.EntityFrameworkCore;

namespace Aldaris.API.Infrastructure;

public class InferenceEngineFactory
{
    private readonly AldarisContext _context;
    private readonly ClauseParser _clauseParser;

    public InferenceEngineFactory(
        AldarisContext context,
        ClauseParser clauseParser
    )
    {
        _context = context;
        _clauseParser = clauseParser;
    }

    public RuleInferenceEngine ConstructInferenceEngine(Guid sessionId)
    {
        var engine = new RuleInferenceEngine();

        var rules = _context.Rules.Include(r => r.Consequent)
            .Include(r => r.Antecedents)
            .AsNoTracking()
            .ToArray();

        foreach (var knowledgeBaseRule in rules)
        {
            var rule = new Rule(_clauseParser.Parse(knowledgeBaseRule.Consequent.Expression).Single());
            foreach (var antecedent in knowledgeBaseRule.Antecedents)
            {
                foreach (var clause in _clauseParser.Parse(antecedent.Expression))
                {
                    rule.AddAntecedent(clause);
                }
            }

            engine.AddRule(rule);
        }

        var session = _context.GameSessions
            .Include(s => s.GameSessionAnswers)
            .ThenInclude(s => s.Question)
            .ThenInclude(q => q.PossibleAnswers)
            .Include(s => s.GameSessionAnswers)
            .ThenInclude(s => s.Answer)
            .AsNoTracking()
            .First(s => s.Id == sessionId);

        foreach (var gameSessionAnswer in session.GameSessionAnswers.Where(s => s.AnswerId != null))
        {
            foreach (var clause in _clauseParser.Parse(gameSessionAnswer.Question, gameSessionAnswer.Answer!))
            {
                engine.AddFact(clause);
            }
        }

        return engine;
    }
}