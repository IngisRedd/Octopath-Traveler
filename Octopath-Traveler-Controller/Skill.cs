using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class Skill
{
    private GameState _gameState;
    private ITargetSelector _targetSelector;
    private List<ISkillEffect> _skillEffects;

    public Skill(GameState gameState, ITargetSelector targetSelector, List<ISkillEffect> skillEffects)
    {
        _gameState = gameState;
        _targetSelector = targetSelector;
        _skillEffects = skillEffects;
    }

    public void Use()
    {
        SelectTarget();
        ApplyEffects();
    }

    public void SelectTarget()
    {
        _gameState.CombatTargets.Clear();
        _targetSelector.Select();
    }

    public void ApplyEffects()
    {
        foreach (ISkillEffect effect in _skillEffects)
        {
            effect.Apply();
        }
    }
    
}