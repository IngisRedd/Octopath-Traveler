using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public abstract class SkillFactory
{
    public abstract ISkill Create(SkillInfo skillInfo);
    protected bool IsItADamagingSkill(SkillInfo skillInfo)
        => skillInfo.Type != DamageType.None;
}