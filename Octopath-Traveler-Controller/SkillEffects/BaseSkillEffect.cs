using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public abstract class BaseSkillEffect : ISkillEffect
{
    protected GameState _gameState;

    public BaseSkillEffect(GameState gameState)
    {
        _gameState = gameState;
    }
    
    public void Apply()
    {
        foreach (CombatUnit target in _gameState.CombatTargets)
        {
            ApplyEffectTo(target);
        }
    }
    
    protected abstract void ApplyEffectTo(CombatUnit target);
}