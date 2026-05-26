using System.Collections;

namespace Octopath_Traveler_Model;

public class TurnQueue
{
    private List<TurnEntry> _entries = new();
    private List<CombatUnit> _orderedUnits => TurnQueueSorter.GetOrderedQueue(_entries);
    public int Count => _entries.Count;
    
    public void Add(CombatUnit unit)
    {
        _entries.Add(new TurnEntry(unit));
    }
    
    public IEnumerator<CombatUnit> GetEnumerator()
    {
        return _orderedUnits.GetEnumerator();
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
            return _orderedUnits[index];
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
        var ordered = _orderedUnits;
        var unitToRemove = ordered[index];

        _entries.RemoveAll(entry => entry.Unit == unitToRemove);
    }
    
    public bool Remove(CombatUnit unit)
    {
        return _entries.RemoveAll(entry => entry.Unit == unit) > 0;
    }
    
    public void RemoveAll(Func<CombatUnit, bool> predicate)
    {
        _entries.RemoveAll(entry => predicate(entry.Unit));
    }

    public void ApplyPriority(CombatUnit unit, TurnPriorityLevel priority)
    {
        TurnEntry entry = _entries.FirstOrDefault(entry => entry.Unit == unit);
        if (entry != null)
        {
            entry.ApplyPriority(priority);
        }
    }
}