namespace Aldaris.ExpertSystem.Clauses;

public abstract class BaseClause
{
    public string Variable  { get; }

    public string Value { get; }

    public string Condition { get; protected init; } = "=";


    protected BaseClause(string variable, string value)
    {
        Variable = variable;
        Value = value;
    }

    protected BaseClause(string variable, string condition, string value)
    {
        Variable = variable;
        Value = value;
        Condition = condition;
    }


    public IntersectionType MatchClause(BaseClause rhs)
    {
        if (Variable != rhs.Variable)
        {
            return IntersectionType.Unknown;
        }

        return Intersect(rhs);
    }

    protected virtual IntersectionType Intersect(BaseClause rhs)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return Variable + " " + Condition + " " + Value;
    }
}