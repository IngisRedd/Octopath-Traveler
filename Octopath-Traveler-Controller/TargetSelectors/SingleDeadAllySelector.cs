using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class SingleDeadAllySelector : BaseSelector
{
    private IRoundView _view;
    
    public SingleDeadAllySelector(GameState gameState, IRoundView view)
        : base(gameState)
    {
        _view = view;
    }

    protected override void OnSelect()
    {
        Traveler target = _view.SelectTravelerAllyTarget(_gameState.TravelerTeam.DeadUnits);
        _gameState.CombatTargets.Add(target);
    }

}