using System.Collections;

namespace Octopath_Traveler_Model;

public class TurnQueue : IEnumerable<CombatUnit>
{
    private List<TurnEntry> _entries = new();
    public int Count => _entries.Count;
    
    public void Add(CombatUnit unit)
    {
        _entries.Add(new TurnEntry(unit));
    }
    
    public IEnumerator<CombatUnit> GetEnumerator()
    {
        return GetOrderedQueue().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public void AddRange(IEnumerable<CombatUnit> units)
    {
        foreach (var unit in units)
        {
            Add(unit);
        }
    }
    
    public CombatUnit this[int index]
    {
        get
        {
            return GetOrderedQueue()[index];
        }
    }
    
    public void Clear()
    {
        _entries.Clear();
    }
    
    public TurnQueue Copy()
    {
        var newQueue = new TurnQueue();

        foreach (var entry in _entries)
        {
            var newEntry = new TurnEntry(entry.Unit);
            newEntry.ApplyPriority(entry.Priority);

            newQueue._entries.Add(newEntry);
        }

        return newQueue;
    }
    
    public void RemoveAt(int index)
    {
        var ordered = GetOrderedQueue();
        var unitToRemove = ordered[index];

        _entries.RemoveAll(e => e.Unit == unitToRemove);
    }
    
    public bool Remove(CombatUnit unit)
    {
        return _entries.RemoveAll(e => e.Unit == unit) > 0;
    }
    
    public void RemoveAll(Func<CombatUnit, bool> predicate)
    {
        _entries.RemoveAll(e => predicate(e.Unit));
    }

    public void ApplyPriority(CombatUnit unit, TurnPriorityLevel priority)
    {
        TurnEntry entry = _entries.First(e => e.Unit == unit);
        entry.ApplyPriority(priority);
    }

    private List<CombatUnit> GetOrderedQueue()
    {
        // Este trainwreck no se puede evitar porque ThenByDescending() requiere ir directamente después de OrderBy
        IEnumerable<TurnEntry> orderedQueue = _entries
            .OrderByDescending(e => e.Priority)
            .ThenByDescending(e => IsTravelerPriorityApplicable(e))
            .ThenByDescending(e => e.Unit.Speed);
        IEnumerable<CombatUnit> units = orderedQueue.Select(e => e.Unit);

        return units.ToList();
    }
    
    private bool IsTravelerPriorityApplicable(TurnEntry entry)
    {
        if (entry.Priority == TurnPriorityLevel.Normal)
            return false;

        return entry.Unit is Traveler;
    }
}