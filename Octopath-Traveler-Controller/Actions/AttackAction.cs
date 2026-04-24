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
        _view.ShowFinalHP(attackTarget); ///Malisismo, hay que quitar de alguna maneraa, tal vez pasando la info a la view para que la view haga magia
    }

    private string SelectWeapon()
    {
        _view.ShowAvailableWeapons();
        int selectedIndex = _view.ReadPlayerInput() - 1;
        return _gameState.CurrentTraveler.Weapons[selectedIndex];
    }
 }