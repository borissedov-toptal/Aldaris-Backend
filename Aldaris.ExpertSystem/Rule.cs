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

    public void FirstAntecedent()
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


    public void Fire(WorkingMemory workingMemory)
    {
        if (!workingMemory.IsFact(Consequent))
        {
            workingMemory.AddFact(Consequent);
        }

        _fired = true;
    }

    public bool IsTriggered(WorkingMemory wm)
    {
        foreach (BaseClause antecedent in _antecedents)
        {
            if (!wm.IsFact(antecedent))
            {
                return false;
            }
        }

        return true;
    }
}