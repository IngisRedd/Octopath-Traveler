using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Exceptions;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public class BattleController
{
    public bool IsGameStillGoing = true;
    private IRoundView _roundView;
    private ICombatActionView _combatActionView;
    private GameState _gameState;

    public BattleController(GameState gameState, IViewFactory viewFactory)
    {
        _gameState = gameState;
        _roundView = viewFactory.CreateRoundView(gameState);
        _combatActionView = viewFactory.CreateCombatActionView(gameState);
    }
    
    public void ExecuteBattleRound()
    {
        GameStateUpdater.PerformStartOfRoundUpdates(_gameState);
        _roundView.StartOfRoundUpdate();
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
        _roundView.StartOfTurnUpdate();
        
        ITurnController turnController = TurnControllerFactory.Create(_gameState, _roundView, _combatActionView);
        turnController.Execute();
        
        GameStateUpdater.EndOfTurnUpdate(_gameState);
        EndOfGameValidator.CheckIfGameIsOver(_gameState, _roundView);
    }
}