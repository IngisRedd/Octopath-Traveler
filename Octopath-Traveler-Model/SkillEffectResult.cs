using Microsoft.VisualBasic.CompilerServices;

namespace Octopath_Traveler_Model;

public class SkillEffectResult
{
    public List<CombatUnit> Targets = new();
    public List<Damage> Damages = new();
    public List<bool> IsBreakingPointAchieved = new();
    public List<bool> IsTravelerDefending = new();
    public List<int?> HealValues = new();
    public List<bool> IsTravelerResurrected = new();

    public SkillEffectResult(List<CombatUnit> targets)
    {
        Targets = targets;
    }
    
    public void AddDefaultEntry()
    {
        Damages.Add(null);
        IsBreakingPointAchieved.Add(false);
        IsTravelerDefending.Add(false);
        HealValues.Add(null);
        IsTravelerResurrected.Add(false);
    }

    public SkillEffectResult GetOrderedSkillEffectResultCurrentUnitAtTheEnd(CombatUnit currentUnit)
    {
        SkillEffectResult newResult = DeepCopy();
        if (Targets.Contains(currentUnit))
        {
            int index = Targets.IndexOf(currentUnit);
            
            MoveItemInIndexToEnd(newResult.Targets, index);
            MoveItemInIndexToEnd(newResult.Damages, index);
            MoveItemInIndexToEnd(newResult.IsBreakingPointAchieved, index);
            MoveItemInIndexToEnd(newResult.IsTravelerDefending, index);
            MoveItemInIndexToEnd(newResult.HealValues, index);
            MoveItemInIndexToEnd(newResult.IsTravelerResurrected, index);
        }
        return newResult;   
    }
    
    private SkillEffectResult DeepCopy()
    {
        return new SkillEffectResult(this.Targets)
        {
            Damages = new List<Damage>(this.Damages),
            IsBreakingPointAchieved = new List<bool>(this.IsBreakingPointAchieved),
            IsTravelerDefending = new List<bool>(this.IsTravelerDefending),
            HealValues = new List<int?>(this.HealValues),
            IsTravelerResurrected = new List<bool>(this.IsTravelerResurrected)
        };
    }

    private void MoveItemInIndexToEnd<T>(List<T> list, int index)
    {
        if (index < 0 || index >= list.Count) return;

        var item = list[index];
        list.RemoveAt(index);
        list.Add(item);
    }
}