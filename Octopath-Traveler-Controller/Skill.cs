using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class Skill
{
    private ITargetSelector _targetSelector;
    private List<ISkillEffect> _skillEffects;

    public Skill(ITargetSelector targetSelector, List<ISkillEffect> skillEffects)
    {
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