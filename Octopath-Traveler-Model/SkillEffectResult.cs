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
    
    public SkillEffectResult DeepCopy()
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
}