using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class MercyStrikeSkillEffect : SkillEffectWithModifier
{
    private DamageType _damageType;

    public MercyStrikeSkillEffect(GameState gameState, decimal modifier, DamageType damageType)
        : base(gameState, modifier)
    {
        _damageType = damageType;
    }

    public override void ApplyTo(CombatUnit target)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(_modifier, _gameState.CurrentUnit, target, _damageType);
        Damage damage = damageCalculator.Calculate();
        damage = MercyStrikeEffect.Apply(damage, target);
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
    }
}