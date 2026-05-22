using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class LastStandSkillEffect : BaseSkillEffect
{
    private decimal _modifier;
    private DamageType _damageType;

    public LastStandSkillEffect(GameState gameState, decimal modifier, DamageType damageType)
        : base(gameState)
    {
        _modifier = modifier;
        _damageType = damageType;
    }

    protected override void ApplyEffectTo(CombatUnit target)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(_modifier, _gameState.CurrentUnit, target, _damageType);
        Damage damage = damageCalculator.Calculate();
        damage = LastStandBonusEffect.Apply(damage, _gameState.CurrentUnit);
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
    }
}