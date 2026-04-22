using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Exceptions;

namespace Octopath_Traveler.Actions;

public class FleeAction : CombatAction
{
    public FleeAction(GameState gameState, MainConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        _view.ShowFleeMessage();
        throw new GameOverException("Player team surrendered");
    }
}