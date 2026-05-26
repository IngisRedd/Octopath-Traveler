using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class SingleDeadAllySelector : ITargetSelector
{
    private GameState _gameState;
    private IRoundView _view;
    
    public SingleDeadAllySelector(GameState gameState, IRoundView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Select()
    {
        Traveler target = _view.SelectTravelerAllyTarget(_gameState.TravelerTeam.DeadUnits);
        _gameState.CombatTargets.Add(target);
    }

}