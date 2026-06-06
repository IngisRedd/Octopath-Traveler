using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class StealSPEffect : SkillEffectWithModifier
{
    private DamageType _damageType = DamageType.Dagger;

    public StealSPEffect(GameState gameState, decimal modifier)
        : base(gameState, modifier){}

    public override void ApplyTo(CombatUnit target)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(_modifier, _gameState.CurrentUnit, target, _damageType);
        Damage damage = damageCalculator.Calculate();
   
        DamageApplier damageApplier = new DamageApplier(_gameState, damage);
        damageApplier.Apply(target);
        
        int spValue = damage.Value / 20;
        _gameState.CurrentTraveler.CurrentSP += spValue;
        
        RegisterSPRecovery(spValue);
    }

    private void RegisterSPRecovery(int spValue)
    {
        throw new NotImplementedException();
    }
}