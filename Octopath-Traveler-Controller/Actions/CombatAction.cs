using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public abstract class CombatAction
{
    protected GameState _gameState;
    protected GameConsoleView _view;
    
    protected CombatAction(GameState gameState, GameConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public abstract void Execute();
}