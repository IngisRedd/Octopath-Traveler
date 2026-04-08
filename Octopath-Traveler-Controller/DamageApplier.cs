using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class DamageApplier
{
    private GameState _gameState;
    private MainConsoleView _view;

    public DamageApplier(GameState gameState, MainConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }
    
    public void MakeBasicAttack(CombatUnit target, DamageType weapon)
    {
        double basicAttackModifier = 1.3;
        DamageCalculator damageCalculator =
            new DamageCalculator(basicAttackModifier, _gameState.CurrentUnit, target, weapon);
        Damage damage = damageCalculator.Calculate();
        AttackTarget(target, damage);
        _view.ShowAttackResults(target, damage);
    }
    
    public void UseDamagingSkill(CombatUnit target, DamageType type, double modifier)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(modifier, _gameState.CurrentUnit, target, type);
        Damage damage = damageCalculator.Calculate();
        AttackTarget(target, damage);
        _view.ShowAttackResults(target, damage);
    }
    
    private void AttackTarget(CombatUnit target, Damage damage)
    {
        target.CurrentHP -= damage.Value;
    }

}