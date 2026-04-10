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
        DamageTarget(target, damage);
        ShowBasicAttackResults(target, damage);
    }

    private void ShowBasicAttackResults(CombatUnit target, Damage damage)
    {
        _view.ShowBasicAttack();
        _view.ShowDamageReceived(target, damage);
        _view.ShowFinalHP(target);
    }
    
    public void UseDamagingSkill(List<Beast> targets, DamageType type, double modifier)
    {
        foreach (CombatUnit target in targets)
        {
            DamageCalculator damageCalculator =
                new DamageCalculator(modifier, _gameState.CurrentUnit, target, type);
            Damage damage = damageCalculator.Calculate();
            DamageTarget(target, damage);
            _view.ShowDamageReceived(target, damage);
        }

        foreach (CombatUnit target in targets)
        {
            _view.ShowFinalHP(target);
        }
    }
    
    private void DamageTarget(CombatUnit target, Damage damage)
    {
        target.CurrentHP -= damage.Value;
    }

}