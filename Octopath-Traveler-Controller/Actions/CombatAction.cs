using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public abstract class CombatAction
{
    protected GameState _gameState;
    protected MainConsoleView _view;
    
    protected CombatAction(GameState gameState, MainConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public abstract void Execute();
}