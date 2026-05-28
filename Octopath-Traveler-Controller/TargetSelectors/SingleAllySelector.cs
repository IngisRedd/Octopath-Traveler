using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class SingleAllySelector : BaseSelector
{
    private IRoundView _view;
    
    public SingleAllySelector(GameState gameState, IRoundView view)
        : base(gameState)
    {
        _view = view;
    }

    protected override void OnSelect()
    {
        Traveler target = _view.SelectTravelerAllyTarget(_gameState.TravelerTeam.AliveUnits);
        _gameState.CombatTargets.Add(target);
    }

}