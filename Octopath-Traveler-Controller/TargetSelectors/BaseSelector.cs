using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public abstract class BaseSelector : ITargetSelector
{
    protected GameState _gameState;
    
    protected BaseSelector(GameState gameState)
    {
        _gameState = gameState;
    }
    
    public void Select()
    {
        _gameState.CombatTargets.Clear();
        OnSelect();
    }

    protected abstract void OnSelect();
}