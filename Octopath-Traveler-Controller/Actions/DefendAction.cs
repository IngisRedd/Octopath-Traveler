using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class DefendAction : CombatAction
{
    public DefendAction(GameState gameState, MainConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        _gameState.CurrentUnit.StatusEffects[StatusType.Defend].Duration = 1;
        MoveCurrentUnitToFrontOfNextTurnQueue();        
    }

    private void MoveCurrentUnitToFrontOfNextTurnQueue()
    {
        _gameState.NextTurnQueue.Remove(_gameState.CurrentUnit);
        _gameState.NextTurnQueue.Insert(0, _gameState.CurrentUnit);
    }
}