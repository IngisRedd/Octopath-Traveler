using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class HalveHPSkillEffect : BaseSkillEffect
{
    public HalveHPSkillEffect(GameState gameState, string skillName)
        : base(gameState, skillName){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        HPHalverDamageCalculator damageCalculator =
            new HPHalverDamageCalculator(target);
        Damage damage = damageCalculator.Calculate();
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
    }
}