using Aldaris.API.Data;
using Aldaris.ExpertSystem;
using Microsoft.EntityFrameworkCore;

namespace Aldaris.API.Infrastructure;

public class KnowledgeBaseFactory
{
    private readonly AldarisContext _context;
    private readonly ClauseParser _clauseParser;
    
    public KnowledgeBaseFactory(
        AldarisContext context, 
        ClauseParser clauseParser
        )
    {
        _context = context;
        _clauseParser = clauseParser;
    }

    public RuleInferenceEngine ConstructInferenceEngine()
    {
       var engine = new RuleInferenceEngine();

        var rules = _context.Rules.Include(r => r.Consequent)
            .Include(r => r.Antecedents)
            .AsNoTracking()
            .ToArray();

        foreach (var knowledgeBaseRule in rules)
        {
            var rule = new Rule(_clauseParser.Parse(knowledgeBaseRule.Consequent.Expression));
            foreach (var antecedent in knowledgeBaseRule.Antecedents)
            {
                rule.AddAntecedent( _clauseParser.Parse(antecedent.Expression));
            }
            engine.AddRule(rule);
        }

        return engine;
    }
}