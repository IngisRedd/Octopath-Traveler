using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class TravelerSingleEnemySelector : BaseSelector
{
    private IRoundView _view;
    
    public TravelerSingleEnemySelector(GameState gameState, IRoundView view)
        : base(gameState)
    {
        _view = view;
    }

    protected override void OnSelect()
    {
        Beast target = _view.SelectEnemyBeastTarget();
        _gameState.CombatTargets.Add(target);
    }

}