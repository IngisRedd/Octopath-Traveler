using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public abstract class BaseSkillEffect : ISkillEffect
{
    protected GameState _gameState;
    private string _skillName;

    public BaseSkillEffect(GameState gameState, string skillName)
    {
        _gameState = gameState;
        _skillName = skillName;
    }
    
    public void Apply()
    {
        InitializeNewSkillEffectResult();
        foreach (CombatUnit target in _gameState.CombatTargets)
        {
            _gameState.LastSkillEffectResult.AddDefaultEntry();
            ApplyEffectTo(target);
        }

        RegisterSkillUsed();
    }

    private void InitializeNewSkillEffectResult()
    {
        List<CombatUnit> targets = new List<CombatUnit>(_gameState.CombatTargets);
        _gameState.AppliedSkillEffectResults.Add(new SkillEffectResult(targets));
    }
    
    protected abstract void ApplyEffectTo(CombatUnit target);

    private void RegisterSkillUsed()
    {
        _gameState.SkillUsedName = _skillName;
    }
}