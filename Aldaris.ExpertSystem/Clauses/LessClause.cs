namespace Aldaris.ExpertSystem.Clauses
{
    public class LessClause : BaseClause
    {
        public LessClause(String variable, String value)
            : base(variable, value)
        {
            Condition = "<";
        }

        protected override IntersectionType Intersect(BaseClause rhs)
        {
            String v1 = Value;
            String v2 = rhs.Value;

            double a = 0;
            double b = 0;

            if (double.TryParse(v1, out a) && double.TryParse(v2, out b))
            {
                if (rhs is LessClause)
                {
                    //v1 < a
                    //v2 < b 
                    //matched: b <= a
                    //unmatched: b > a
                    if (b <= a)
                    {
                        return IntersectionType.INCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }

                if (rhs is LessOrEqualClause)
                {
                    //v1 < a
                    //v2 <= b 
                    //matched: b < a
                    //unmatched: b >= a
                    if (b < a)
                    {
                        return IntersectionType.INCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }
                if (rhs is IsClause)
                {
                    //v1 < a
                    //v2 = b
                    //matched: b < a
                    //mutually exclusive: b >= a
                    if (b < a)
                    {
                        return IntersectionType.INCLUDE;
                    }

                    return IntersectionType.MUTUALLY_EXCLUDE;
                }
                if (rhs is GreaterOrEqualClause)
                {
                    //v1 < a
                    //v2 >= b
                    //mutually exclusive: b >= a
                    //unmatched: b < a
                    if (b >= a)
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }
                if (rhs is GreaterClause)
                {
                    //v1 < a
                    //v2 > b
                    //mutually exclusive: b >= a
                    //unmatched: b < a
                    if (b >= a)
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }
                return IntersectionType.UNKNOWN;
            }

            return IntersectionType.UNKNOWN;
        }
    }
}