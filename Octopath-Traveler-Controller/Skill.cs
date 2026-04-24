using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class Skill
{
    public ITargetSelector TargetSelector;
    public ISkillEffect SkillEffect;

    public Skill(ITargetSelector targetSelector, ISkillEffect skillEffect)
    {
        TargetSelector = targetSelector;
        SkillEffect = skillEffect;
    }

    public void Use()
    {
        TargetSelector.Select();
        SkillEffect.Apply();
    }
}