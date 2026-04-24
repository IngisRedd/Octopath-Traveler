using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class TravelerSingleEnemySelector : ITargetSelector
{
    private GameState _gameState;
    private GameConsoleView _view;
    
    public TravelerSingleEnemySelector(GameState gameState, GameConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Select()
    {
        Beast target = _view.SelectEnemyBeastTarget();
        _gameState.CombatTargets.Add(target);
    }

}