using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class AttackAction : CombatAction
{
    public AttackAction(GameState gameState, MainConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        string selectedWeapon = SelectWeapon();
        Beast attackTarget = SelectTarget();
        int BPToUse = AskForBPToUseIfAvailable();

        DamageApplier damageApplier = new DamageApplier(_gameState, _view);
        damageApplier.MakeBasicAttack(attackTarget, selectedWeapon);
    }

    private string SelectWeapon()
    {
        _view.ShowAvailableWeapons();
        int selectedIndex = Utils.ReadPlayerInput(_view) - 1;
        return _gameState.CurrentTraveler.Weapons[selectedIndex];
    }
    
    private Beast SelectTarget()
    {
        _view.ShowAvailableTargets();
        int selectedIndex = Utils.ReadPlayerInput(_view) - 1;
        return _gameState.BeastTeam.AliveUnits[selectedIndex];
    }

    private int AskForBPToUseIfAvailable()
    {
        if (!AreThereAnyBPLeft())
            return 0;

        _view.AskForBPUsage();
        return Utils.ReadPlayerInput(_view);
    }
    private bool AreThereAnyBPLeft()
        => (_gameState.CurrentTraveler.BP > 0);
}