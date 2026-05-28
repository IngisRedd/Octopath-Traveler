using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class DamageSkillEffect : SkillEffectWithModifier
{
    private DamageType _damageType;

    public DamageSkillEffect(GameState gameState, string skillName, decimal modifier, DamageType damageType)
        : base(gameState, skillName, modifier)
    {
        _damageType = damageType;
    }

    protected override void ApplyEffectTo(CombatUnit target)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(_modifier, _gameState.CurrentUnit, target, _damageType);
        Damage damage = damageCalculator.Calculate();
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
    }
}