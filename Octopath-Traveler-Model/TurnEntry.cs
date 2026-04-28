namespace Octopath_Traveler_Model;

public class TurnEntry
{
    public CombatUnit Unit { get; }
    public TurnPriorityLevel Priority { get; private set; } = TurnPriorityLevel.Normal;

    public TurnEntry(CombatUnit unit)
    {
        Unit = unit;
    }

    public void ApplyPriority(TurnPriorityLevel newPriority)
    {
        if (IsPriorityGoingFromNormalToMinimum(newPriority))
        {
            Priority = newPriority;
        } 
        else if (newPriority > Priority)
        {
            Priority = newPriority;
        }
    }

    private bool IsPriorityGoingFromNormalToMinimum(TurnPriorityLevel newPriority)
        => Priority == TurnPriorityLevel.Normal && newPriority == TurnPriorityLevel.Minimun;
}