using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class TargetEffectsMap
{
    public List<CombatUnit> DistinctTargetsInOrder { get; } = new List<CombatUnit>();
    public Dictionary<CombatUnit, List<TargetedEffect>> EffectsByTarget { get; } = new Dictionary<CombatUnit, List<TargetedEffect>>();

    public void AddEffect(CombatUnit target, SkillEffectResult result, int index)
    {
        if (!EffectsByTarget.ContainsKey(target))
        {
            DistinctTargetsInOrder.Add(target);
            EffectsByTarget[target] = new List<TargetedEffect>();
        }

        TargetedEffect wrapper = new TargetedEffect(result, index);
        EffectsByTarget[target].Add(wrapper);
    }
}