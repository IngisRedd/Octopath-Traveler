using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Exceptions;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public class BattleController
{
    public bool IsGameStillGoing = true;
    private RoundConsoleView _roundConsoleView;
    private CombatActionConsoleView _combatActionView;
    private GameState _gameState;

    public BattleController(GameState gameState, RoundConsoleView roundConsoleView, CombatActionConsoleView combatActionView)
    {
        _gameState = gameState;
        _roundConsoleView = roundConsoleView;
        _combatActionView = combatActionView;
    }
    
    public void ExecuteBattleRound()
    {
        GameStateUpdater.PerformStartOfRoundUpdates(_gameState);
        _roundConsoleView.ShowRoundStart();
        try
        {
            while (_gameState.IsTurnStillGoing)
            {
                ExecuteTurn();
            }
        }
        catch (GameOverException exception)
        {
            IsGameStillGoing = false;
        }
        GameStateUpdater.PerformEndOfRoundUpdates(_gameState);
    }
    
    private void ExecuteTurn()
    {
        GameStateUpdater.UpdateCurrentUnit(_gameState);
        _roundConsoleView.ShowTurnInfo();
        
        ITurnController turnController = TurnControllerFactory.Create(_gameState, _roundConsoleView, _combatActionView);
        turnController.Execute();
        
        GameStateUpdater.EndOfTurnUpdate(_gameState);
        EndOfGameValidator.CheckIfGameIsOver(_gameState, _roundConsoleView);
    }
}