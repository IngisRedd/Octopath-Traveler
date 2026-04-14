using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class BeastTargetSelector
{
    public static Traveler SelectTarget(
        IEnumerable<Traveler> travelers,
        Stat stat,
        SelectionType selection)
    {
        Func<Traveler, int> selector = stat switch
        {
            Stat.HP => t => t.CurrentHP,
            Stat.PhysAtk => t => t.PhysAtk,
            Stat.PhysDef => t => t.PhysDef,
            Stat.ElemAtk => t => t.ElemAtk,
            Stat.ElemDef => t => t.ElemDef,
            Stat.Speed => t => t.Speed,
            _ => throw new ArgumentOutOfRangeException()
        };

        return selection == SelectionType.Highest
            ? travelers.MaxBy(selector)
            : travelers.MinBy(selector);
    }
}