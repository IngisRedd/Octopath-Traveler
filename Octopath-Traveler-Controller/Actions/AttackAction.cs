using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class AttackAction : CombatAction
{
    public AttackAction(GameState gameState, MainConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        DamageType selectedWeapon = Utils.ParseDamageType(SelectWeapon());
        Beast attackTarget = _view.SelectTarget();
        int BPToUse = _view.AskForBPToUseIfAvailable();

        DamageApplier damageApplier = new DamageApplier(_gameState, _view);
        _view.ShowBasicAttack();
        damageApplier.MakeBasicAttack(attackTarget, selectedWeapon);
    }

    private string SelectWeapon()
    {
        _view.ShowAvailableWeapons();
        int selectedIndex = _view.ReadPlayerInput() - 1;
        return _gameState.CurrentTraveler.Weapons[selectedIndex];
    }
 }