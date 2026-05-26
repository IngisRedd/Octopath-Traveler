namespace Octopath_Traveler_Model;

public static class TurnQueueSorter
{
    public static List<CombatUnit> GetOrderedQueue(List<TurnEntry> entries)
    {
        // Este trainwreck no se puede evitar porque ThenByDescending() requiere ir directamente después de OrderBy
        IEnumerable<TurnEntry> orderedQueue = entries
            .OrderByDescending(entry => entry.Priority)
            .ThenByDescending(entry => IsTravelerPriorityApplicable(entry))
            .ThenByDescending(entry => entry.Unit.Speed);
        IEnumerable<CombatUnit> units = orderedQueue.Select(entry => entry.Unit);

        return units.ToList();
    }
    
    private static bool IsTravelerPriorityApplicable(TurnEntry entry)
    {
        if (entry.Priority == TurnPriorityLevel.Normal)
            return false;

        return entry.Unit is Traveler;
    }

}