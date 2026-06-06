using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public abstract class BaseSkillEffect : ISkillEffect
{
    protected GameState _gameState;

    public BaseSkillEffect(GameState gameState)
    {
        _gameState = gameState;
    }
    
    public abstract void ApplyTo(CombatUnit target);
}