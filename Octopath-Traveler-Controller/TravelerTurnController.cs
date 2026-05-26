using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public class TravelerTurnController : ITurnController
{
    private GameState _gameState;
    private RoundConsoleView _roundConsoleView;
    private CombatActionConsoleView _combatActionConsoleView;
    
    public TravelerTurnController(GameState gameState, RoundConsoleView roundConsoleView,
        CombatActionConsoleView combatActionConsoleView)
    {
        _gameState = gameState;
        _roundConsoleView = roundConsoleView;
        _combatActionConsoleView = combatActionConsoleView;
    }
    
    public void Execute()
    {
        bool isValidActionSelected = false;
        while (!isValidActionSelected)
        {
            try
            {
                CombatActionType actionType = _roundConsoleView.SelectTravelerCombatAction();
                ExecuteTravelerAction(actionType);
                isValidActionSelected = true;
            }
            catch (ArgumentOutOfRangeException exception){}
        }
    }

    private void ExecuteTravelerAction(CombatActionType actionType)
    {
        CombatAction combatAction = CombatActionFactory.Create(actionType, _gameState, _roundConsoleView, _combatActionConsoleView);
        combatAction.Execute();
    }

}