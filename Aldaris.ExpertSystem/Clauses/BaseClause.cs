namespace Aldaris.ExpertSystem.Clauses;

public abstract class BaseClause
{
    private readonly string _variable;
    public string Value { get; }

    public string Condition { get; protected set; } = "=";

    public String Variable => _variable;

    protected BaseClause(string variable, string value)
    {
        _variable = variable;
        Value = value;
    }

    protected BaseClause(string variable, string condition, string value)
    {
        _variable = variable;
        Value = value;
        Condition = condition;
    }


    public IntersectionType MatchClause(BaseClause rhs)
    {
        if (_variable != rhs.Variable)
        {
            return IntersectionType.UNKNOWN;
        }

        return Intersect(rhs);
    }

    protected virtual IntersectionType Intersect(BaseClause rhs)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return _variable + " " + Condition + " " + Value;
    }
}