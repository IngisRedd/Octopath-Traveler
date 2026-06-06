using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class HPThiefEffect : SkillEffectWithModifier
{
    private DamageType _damageType = DamageType.Dagger;

    public HPThiefEffect(GameState gameState, decimal modifier)
        : base(gameState, modifier){}

    public override void ApplyTo(CombatUnit target)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(_modifier, _gameState.CurrentUnit, target, _damageType);
        Damage damage = damageCalculator.Calculate();
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
        
        int healValue = damage.Value / 2;
        _gameState.CurrentUnit.CurrentHP += healValue;
        HealingSkillEffect.RegisterHealing(_gameState, healValue);
    }
}