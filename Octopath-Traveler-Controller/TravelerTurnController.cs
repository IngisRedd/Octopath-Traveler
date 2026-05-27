using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Exceptions;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public class TravelerTurnController : ITurnController
{
    private GameState _gameState;
    private IRoundView _roundView;
    private ICombatActionView _combatActionView;
    
    public TravelerTurnController(GameState gameState, IRoundView roundView,
        ICombatActionView combatActionView)
    {
        _gameState = gameState;
        _roundView = roundView;
        _combatActionView = combatActionView;
    }
    
    public void Execute()
    {
        bool isValidActionSelected = false;
        while (!isValidActionSelected)
        {
            try
            {
                CombatActionType actionType = _roundView.SelectTravelerCombatAction();
                ExecuteTravelerAction(actionType);
                isValidActionSelected = true;
            }
            catch (SelectionCanceledException exception){}
        }
    }

    private void ExecuteTravelerAction(CombatActionType actionType)
    {
        CombatAction combatAction = CombatActionFactory.Create(actionType, _gameState, _roundView, _combatActionView);
        combatAction.Execute();
    }

}