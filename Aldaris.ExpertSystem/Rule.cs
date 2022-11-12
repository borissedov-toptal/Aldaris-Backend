using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.ExpertSystem;

public class Rule
{
    private readonly string _name;
    private readonly List<BaseClause> _antecedents = new();

    private bool _fired;
    private int _index;

    public Rule(string name, BaseClause consequent)
    {
        _name = name;
        Consequent = consequent;
    }

    public void ResetAntecedentIterator()
    {
        _index = 0;
    }

    public bool HasNextAntecedents()
    {
        return _index < _antecedents.Count;
    }

    public BaseClause NextAntecedent()
    {
        BaseClause c = _antecedents[_index];
        _index++;
        return c;
    }

    public string GetName()
    {
        return _name;
    }

    public void AddAntecedent(BaseClause antecedent)
    {
        _antecedents.Add(antecedent);
    }

    public BaseClause Consequent { get; }

    public bool IsFired()
    {
        return _fired;
    }


    /// <summary>
    /// Checks if Rule's Consequent IsFact for a WorkingMemory and in case of yes saves it into WM
    /// </summary>
    /// <param name="workingMemory"></param>
    public void Fire(WorkingMemory workingMemory)
    {
        if (!workingMemory.IsFact(Consequent))
        {
            workingMemory.AddFact(Consequent);
        }

        _fired = true;
    }

    /// <summary>
    /// Checks if All Rule's Antecedents are Facts for a WorkingMemory
    /// </summary>
    /// <param name="workingMemory"></param>
    /// <returns></returns>
    public bool IsTriggered(WorkingMemory workingMemory)
    {
        // return _antecedents.All(workingMemory.IsFact);
        
        foreach (BaseClause antecedent in _antecedents)
        {
            if (!workingMemory.IsFact(antecedent))
            {
                return false;
            }
        }

        return true;
    }
}