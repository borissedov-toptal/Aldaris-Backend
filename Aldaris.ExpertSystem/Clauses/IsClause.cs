namespace Aldaris.ExpertSystem.Clauses
{
    public class IsClause : BaseClause
    {
        public IsClause(string variable, string value)
            : base(variable, value)
        {
            Condition = "=";
        }

        protected override IntersectionType Intersect(BaseClause rhs)
        {
            if (rhs is IsClause)
            {
                if (Value == rhs.Value)
                {
                    return IntersectionType.Include;
                }

                return IntersectionType.MutuallyExclude;
            }

            string v1 = Value;
            string v2 = rhs.Value;

            double a = 0;
            double b = 0;

            if (double.TryParse(v1, out a) && double.TryParse(v2, out b))
            {
                if (rhs is LessClause)
                {
                    if (a >= b)
                    {
                        return IntersectionType.MutuallyExclude;
                    }

                    return IntersectionType.Include;
                }

                if (rhs is LessOrEqualClause)
                {
                    if (a > b)
                    {
                        return IntersectionType.MutuallyExclude;
                    }

                    return IntersectionType.Include;
                }
                if (rhs is GreaterClause)
                {
                    if (a <= b)
                    {
                        return IntersectionType.MutuallyExclude;
                    }

                    return IntersectionType.Include;
                }
                if (rhs is GreaterOrEqualClause)
                {
                    if (a < b)
                    {
                        return IntersectionType.MutuallyExclude;
                    }

                    return IntersectionType.Include;
                }
                return IntersectionType.Unknown;
            }

            return IntersectionType.Unknown;
        }
    }
}