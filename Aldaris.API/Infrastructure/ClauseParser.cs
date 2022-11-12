using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.API.Infrastructure;

public class ClauseParser
{
    public BaseClause Parse(string expression)
    {
        var parts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3) throw new ClauseParseException("Invalid number of segments");
        BaseClause clause = parts[1] switch
        {
            "=" => new IsClause(parts[0], parts[2]),
            "<" => new LessClause(parts[0], parts[2]),
            "<=" => new LessOrEqualClause(parts[0], parts[2]),
            ">" => new GreaterClause(parts[0], parts[2]),
            ">=" => new GreaterOrEqualClause(parts[0], parts[2]),
            _ => throw new ClauseParseException("Invalid operator specified")
        };

        return clause;
    }

    private class ClauseParseException:Exception
    {
        public ClauseParseException(string message) : base(message){ }
    }
}