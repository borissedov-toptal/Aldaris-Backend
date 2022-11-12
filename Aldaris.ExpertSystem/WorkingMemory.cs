using System.Text;
using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.ExpertSystem;

public class WorkingMemory
{
    private readonly List<BaseClause> _facts = new();

    public void AddFact(BaseClause fact)
    {
        _facts.Add(fact);
    }
    
    public bool IsFact(BaseClause c)
    {
        return _facts.Any(f => f.MatchClause(c) == IntersectionType.Include) 
               && _facts.All(f => f.MatchClause(c) != IntersectionType.MutuallyExclude);
    }

    public bool IsNotFact(BaseClause c)
    {
        return _facts.Any(fact => fact.MatchClause(c) == IntersectionType.MutuallyExclude);
    }

    public void ClearFacts()
    {
        _facts.Clear();
    }


    public override string ToString()
    {
        var message = new StringBuilder();

        bool firstClause = true;
        foreach (BaseClause cc in _facts)
        {
            if (firstClause)
            {
                message.Append(cc);
                firstClause = false;
            }
            else
            {
                message.Append("\n" + cc);
            }
        }

        return message.ToString();
    }

    public int Count => _facts.Count;
}