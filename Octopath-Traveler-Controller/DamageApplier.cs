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
    
    public void MakeBasicAttack(CombatUnit target, string weapon)
    {
        double basicAttackModifier = 1.3;
        Damage damage = new Damage(basicAttackModifier, _gameState.CurrentUnit, target, weapon);
        AttackTarget(target, damage);
        _view.ShowAttackResults(target, damage);
    }
    
    private void AttackTarget(CombatUnit target, Damage damage)
    {
        target.CurrentHP -= damage.Value;
    }

}