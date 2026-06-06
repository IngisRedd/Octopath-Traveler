using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class HalveHPSkillEffect : BaseSkillEffect
{
    public HalveHPSkillEffect(GameState gameState)
        : base(gameState){}

    public override void ApplyTo(CombatUnit target)
    {
        HPHalverDamageCalculator damageCalculator =
            new HPHalverDamageCalculator(target);
        Damage damage = damageCalculator.Calculate();
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
    }
}