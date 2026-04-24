using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class TravelerSingleAllySelector : ITargetSelector
{
    private GameState _gameState;
    private GameConsoleView _view;
    
    public TravelerSingleAllySelector(GameState gameState, GameConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Select()
    {
        Traveler target = _view.SelectTravelerAllyTarget();
        _gameState.CombatTargets.Add(target);
    }

}