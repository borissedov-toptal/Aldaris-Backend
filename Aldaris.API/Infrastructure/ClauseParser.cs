using Aldaris.API.Domain;
using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.API.Infrastructure;

public class ClauseParser
{
    public IEnumerable<BaseClause> Parse(string expression)
    {
        foreach (var subExpression in expression.Split(" && ", StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = subExpression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) throw new ClauseParseException("Invalid number of segments");
            yield return parts[1] switch
            {
                "=" => new IsClause(parts[0], parts[2]),
                "<" => new LessClause(parts[0], parts[2]),
                "<=" => new LessOrEqualClause(parts[0], parts[2]),
                ">" => new GreaterClause(parts[0], parts[2]),
                ">=" => new GreaterOrEqualClause(parts[0], parts[2]),
                _ => throw new ClauseParseException("Invalid operator specified")
            };
        }
    }

    public IEnumerable<BaseClause> Parse(Question question, Answer answer)
    {
        return answer.ClauseType switch
        {
            "=" => new[] { new IsClause(question.VariableName, answer.ClauseValue) },
            "<" => new[] { new LessClause(question.VariableName, answer.ClauseValue) },
            "<=" => new[] { new LessOrEqualClause(question.VariableName, answer.ClauseValue) },
            ">" => new[] { new GreaterClause(question.VariableName, answer.ClauseValue) },
            ">=" => new[] { new GreaterOrEqualClause(question.VariableName, answer.ClauseValue) },
            "&&" => answer.ClauseValue.Split(" && ", StringSplitOptions.RemoveEmptyEntries).SelectMany(Parse),
            _ => throw new ClauseParseException("Invalid operator specified")
        };
    }

    private class ClauseParseException : Exception
    {
        public ClauseParseException(string message) : base(message)
        {
        }
    }
}