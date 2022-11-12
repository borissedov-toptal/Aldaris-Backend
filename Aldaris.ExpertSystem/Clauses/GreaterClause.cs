namespace Aldaris.ExpertSystem.Clauses
{
    public class GreaterClause : BaseClause
    {
        public GreaterClause(string variable, string value)
            : base(variable, value)
        {
            Condition = ">";
        }

        protected override IntersectionType Intersect(BaseClause rhs)
        {
            string v1 = Value;
            string v2 = rhs.Value;

            double a = 0;
            double b = 0;

            if (double.TryParse(v1, out a) && double.TryParse(v2, out b))
            {
                if (rhs is LessClause)
                {
                    //v1 > a
                    //v2 < b 
                    //mutually exclusive: b <= a
                    //unmatched: b > a
                    if (b <= a)
                    {
                        return IntersectionType.MutuallyExclude;
                    }

                    return IntersectionType.Unknown;
                }

                if (rhs is LessOrEqualClause)
                {
                    //v1 > a
                    //v2 <= b 
                    //matched: b <= a
                    //unmatched: b > a
                    if (b <= a)
                    {
                        return IntersectionType.MutuallyExclude;
                    }

                    return IntersectionType.Unknown;
                }
                if (rhs is IsClause)
                {
                    //v1 > a
                    //v2 = b
                    //matched: b > a
                    //mutually exclusive: b <= a
                    if (b > a)
                    {
                        return IntersectionType.Include;
                    }

                    return IntersectionType.MutuallyExclude;
                }
                if (rhs is GreaterOrEqualClause)
                {
                    //v1 > a
                    //v2 >= b
                    //mutually exclusive: b > a
                    //unmatched: b < a
                    if (b > a)
                    {
                        return IntersectionType.Include;
                    }

                    return IntersectionType.Unknown;
                }
                if (rhs is GreaterClause)
                {
                    //v1 > a
                    //v2 > b
                    //mutually exclusive: b >= a
                    //unmatched: b < a
                    if (b >= a)
                    {
                        return IntersectionType.Include;
                    }

                    return IntersectionType.Unknown;
                }
                return IntersectionType.Unknown;
            }

            return IntersectionType.Unknown;
        }
    }
}