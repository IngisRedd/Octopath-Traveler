using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class BeastTurnController
{
    private GameState _gameState;
    private MainConsoleView _view;

    public BeastTurnController(GameState gameState, MainConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Execute()
    {
        Traveler attackTarget = _gameState.TravelerTeam.HealthiestUnit;
        string damageType = "Physical";
        
        DamageApplier damageApplier = new DamageApplier(_gameState, _view);
        damageApplier.MakeBasicAttack(attackTarget, damageType);

    }
}