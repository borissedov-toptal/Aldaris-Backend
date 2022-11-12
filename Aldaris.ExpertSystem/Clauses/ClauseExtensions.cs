namespace Aldaris.ExpertSystem.Clauses;

public static class ClauseExtensions
{
    public static IEnumerable<BaseClause> Simplify(this IEnumerable<BaseClause> conditions)
    {
        var query = conditions.Distinct(ClauseEqualityComparer.Instance).ToArray();

        var indexesToExclude = new HashSet<int>();
        for (int i = 0; i < query.Length - 1; i++)
        {
            for (int j = i; j < query.Length; j++)
            {
                if (query[i].MatchClause(query[j]) == IntersectionType.MutuallyExclude)
                {
                    indexesToExclude.Add(i);
                    indexesToExclude.Add(j);
                }
            }
        }

        return query.Where((c, i) => !indexesToExclude.Contains(i));
    }
}

public class ClauseEqualityComparer:IEqualityComparer<BaseClause>
{
    private static ClauseEqualityComparer? _instance;
    public static ClauseEqualityComparer Instance => _instance ??= new ClauseEqualityComparer();

    public bool Equals(BaseClause? x, BaseClause? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Variable == y.Variable && x.Value == y.Value && x.Condition == y.Condition;
    }

    public int GetHashCode(BaseClause obj)
    {
        return HashCode.Combine(obj.Variable, obj.Value, obj.Condition);
    }
}