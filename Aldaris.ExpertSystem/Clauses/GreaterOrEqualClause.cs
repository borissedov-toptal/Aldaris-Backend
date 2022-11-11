﻿namespace Aldaris.ExpertSystem.Clauses
{
    public class GreaterOrEqualClause : BaseClause
    {
        public GreaterOrEqualClause(string variable, string value)
            : base(variable, value)
        {
            Condition = ">=";
        }

        protected override IntersectionType Intersect(BaseClause rhs)
        {
            string v1 = Value;
            string v2 = rhs.Value;

            var a = 0.0;
            var b = 0.0;

            if (double.TryParse(v1, out a) && double.TryParse(v2, out b))
            {
                if (rhs is LessClause)
                {
                    //v1 >= a
                    //v2 < b 
                    //mutually exclusive: b <= a
                    //unmatched: b > a
                    if (b <= a)
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }

                if (rhs is LessOrEqualClause)
                {
                    //v1 >= a
                    //v2 < b 
                    //matched: b <= a
                    //unmatched: b > a
                    if (b <= a)
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }
                if (rhs is IsClause)
                {
                    //v1 >= a
                    //v2 = b
                    //matched: b >= a
                    //mutually exclusive: b < a
                    if (b >= a)
                    {
                        return IntersectionType.INCLUDE;
                    }

                    return IntersectionType.MUTUALLY_EXCLUDE;
                }
                if (rhs is GreaterOrEqualClause)
                {
                    //v1 >= a
                    //v2 >= b
                    //mutually exclusive: b >= a
                    //unmatched: b < a
                    if (b >= a)
                    {
                        return IntersectionType.INCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }
                if (rhs is GreaterClause)
                {
                    //v1 >= a
                    //v2 > b
                    //mutually exclusive: b >= a
                    //unmatched: b < a
                    if (b >= a)
                    {
                        return IntersectionType.INCLUDE;
                    }

                    return IntersectionType.UNKNOWN;
                }
                return IntersectionType.UNKNOWN;
            }

            return IntersectionType.UNKNOWN;
        }
    }
}