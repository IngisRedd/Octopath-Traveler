namespace Octopath_Traveler_Model;

public class Combatants
{
    private List<CombatUnit> _units = new();
    public int Count => _units.Count;
    public string[] UnitNames => _units.Select(unit => unit.Name).ToArray();
    public Combatants AliveUnits => new Combatants(_units.Where(beast => beast.CurrentHP > 0));
    public Combatants DeadUnits => new Combatants(_units.Where(beast => beast.CurrentHP == 0));
    
    public Combatants() : this(new CombatUnit[0])
    {
    }

    public Combatants(IEnumerable<CombatUnit> initialUnits)
    {
        _units = new List<CombatUnit>();
        
        foreach (CombatUnit unit in initialUnits)
        {
            _units.Add(unit);
        }
    }
    
    public void Add(CombatUnit unit)
    {
        _units.Add(unit);
    }

    public IEnumerator<CombatUnit> GetEnumerator()
    {
        return _units.GetEnumerator();
    }

    public void AddRange(IEnumerable<CombatUnit> units)
    {
        foreach (var unit in units)
        {
            Add(unit);
        }
    }
    
    public void AddRange(Combatants units)
    {
        foreach (var unit in units)
        {
            Add(unit);
        }
    }

    public CombatUnit this[int index]
    {
        get { return _units[index]; }
    }

    public void Clear()
    {
        _units.Clear();
    }
    
    public CombatUnit FirstOrDefault(Predicate<CombatUnit> matchCondition)
    {
        foreach (CombatUnit unit in _units)
        {
            if (matchCondition(unit))
            {
                return unit;
            }
        }

        return null;
    }
    
    public CombatUnit MaxBy<TKey>(Func<CombatUnit, TKey> selector)
    {
        return _units.MaxBy(selector);
    }

    public CombatUnit MinBy<TKey>(Func<CombatUnit, TKey> selector)
    {
        return _units.MinBy(selector);
    }
}