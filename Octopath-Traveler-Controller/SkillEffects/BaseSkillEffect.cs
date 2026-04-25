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
        InitializeNewSkillEffectResult();
        foreach (CombatUnit target in _gameState.CombatTargets)
        {
            _gameState.LastSkillEffectResult.AddDefaultEntry();
            ApplyEffectTo(target);
        }
    }
    
    protected abstract void ApplyEffectTo(CombatUnit target);

    private void InitializeNewSkillEffectResult()
    {
        List<CombatUnit> targets = new List<CombatUnit>(_gameState.CombatTargets);
        _gameState.SkillEffectResults.Add(new SkillEffectResult(targets));
    }
}