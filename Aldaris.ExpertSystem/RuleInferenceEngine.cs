using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.ExpertSystem;

public class RuleInferenceEngine
{
    private readonly List<Rule> _knowledgeBase = new();
    private readonly WorkingMemory _workingMemory = new();

    public void AddRule(Rule rule)
    {
        _knowledgeBase.Add(rule);
    }

    public void ClearRules()
    {
        _knowledgeBase.Clear();
    }
    
    public WorkingMemory Facts => _workingMemory;

    /// <summary>
    /// Add another know fact into the working memory
    /// </summary>
    /// <param name="c"></param>
    public void AddFact(BaseClause c)
    {
        _workingMemory.AddFact(c);
    }

    public void ClearFacts()
    {
        _workingMemory.ClearFacts();
    }
    
    //forward chain
    public void InferForward()
    {
        List<Rule> matchedRules;
        do
        {
            matchedRules = GetMatchedRules();
            if (matchedRules.Count > 0)
            {
                if (!FireRule(matchedRules))
                {
                    break;
                }
            }
        } while (matchedRules.Count > 0);
    }
    
    /// <summary>
    /// Backward chain
    /// </summary>
    /// <param name="unprovedConditions"></param>
    /// <returns>Conclusion</returns>
    public BaseClause? InferBackward(List<BaseClause> unprovedConditions)
    {
        BaseClause? conclusion = null;

        foreach (Rule rule in _knowledgeBase)
        {
            rule.ResetAntecedentIterator();
            bool goalReached = true;
            while (rule.HasNextAntecedents())
            {
                BaseClause antecedent = rule.NextAntecedent();

                if (!_workingMemory.IsFact(antecedent))
                {
                    if (_workingMemory.IsNotFact(antecedent))
                    {
                        //conflict with what is already known
                        goalReached = false;
                        break;
                    } 
                    if (IsFact(antecedent, unprovedConditions))
                    {
                        //deduce to be a fact
                        _workingMemory.AddFact(antecedent);
                    }
                    else
                    {
                        //deduce to not be a fact
                        goalReached = false;
                        break;
                    }
                }
            }

            if (goalReached)
            {
                conclusion = rule.Consequent;
                break;
            }
        }

        return conclusion;
    }
    
    

    private bool IsFact(BaseClause goal, List<BaseClause> unprovedConditions)
    {
        var goalStack = new Stack<Rule>();

        foreach (Rule rule in _knowledgeBase)
        {
            BaseClause consequent = rule.Consequent;
            IntersectionType it = consequent.MatchClause(goal);
            if (it == IntersectionType.Include)
            {
                goalStack.Push(rule);
            }
        }

        if (goalStack.Count == 0)
        {
            unprovedConditions.Add(goal);
        }
        else
        {
            while (goalStack.TryPop(out var rule))
            {
                rule.ResetAntecedentIterator();
                bool goalReached = true;
                while (rule.HasNextAntecedents())
                {
                    BaseClause antecedent = rule.NextAntecedent();
                    if (!_workingMemory.IsFact(antecedent))
                    {
                        if (_workingMemory.IsNotFact(antecedent))
                        {
                            goalReached = false;
                            break;
                        }
                        if (IsFact(antecedent, unprovedConditions))
                        {
                            _workingMemory.AddFact(antecedent);
                        }
                        else
                        {
                            goalReached = false;
                            break;
                        }
                    }
                }

                if (goalReached)
                {
                    return true;
                }
            } 
        }
        
        //
        // if (goalStack.Count == 0)
        // {
        //     unprovedConditions.Add(goal);
        // }
        // else
        // {
        //     foreach (Rule rule in goalStack)
        //     {
        //         rule.FirstAntecedent();
        //         bool goalReached = true;
        //         while (rule.HasNextAntecedents())
        //         {
        //             BaseClause antecedent = rule.NextAntecedent();
        //             if (!_workingMemory.IsFact(antecedent))
        //             {
        //                 if (_workingMemory.IsNotFact(antecedent))
        //                 {
        //                     goalReached = false;
        //                     break;
        //                 }
        //                 if (IsFact(antecedent, unprovedConditions))
        //                 {
        //                     _workingMemory.AddFact(antecedent);
        //                 }
        //                 else
        //                 {
        //                     goalReached = false;
        //                     break;
        //                 }
        //             }
        //         }
        //
        //         if (goalReached)
        //         {
        //             return true;
        //         }
        //     }
        // }

        return false;
    }

    private bool FireRule(List<Rule> conflictingRules)
    {
        bool hasRuleToFire = false;
        foreach (Rule rule in conflictingRules)
        {
            if (!rule.IsFired())
            {
                hasRuleToFire = true;
                rule.Fire(_workingMemory);
            }
        }

        return hasRuleToFire;
    }


    /// <summary>
    /// Method that return the set of rules whose antecedents match with the working memory
    /// </summary>
    /// <returns></returns>
    private List<Rule> GetMatchedRules()
    {
        List<Rule> cs = new List<Rule>();
        foreach (Rule rule in _knowledgeBase)
        {
            if (rule.IsTriggered(_workingMemory))
            {
                cs.Add(rule);
            }
        }

        return cs;
    }
}