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
        Beast attackTarget = Utils.SelectTarget(_gameState, _view);
        int BPToUse = Utils.AskForBPToUseIfAvailable(_gameState, _view);

        DamageApplier damageApplier = new DamageApplier(_gameState, _view);
        damageApplier.MakeBasicAttack(attackTarget, selectedWeapon);
    }

    private string SelectWeapon()
    {
        _view.ShowAvailableWeapons();
        int selectedIndex = Utils.ReadPlayerInput(_view) - 1;
        return _gameState.CurrentTraveler.Weapons[selectedIndex];
    }
 }