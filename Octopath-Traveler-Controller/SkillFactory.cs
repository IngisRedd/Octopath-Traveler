using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class SkillFactory
{
    public ISkill Create(SkillInfo skillInfo)
    {
        if (skillInfo.Type != DamageType.None)
        {
            return new DamagingSkill(skillInfo.Type, skillInfo.Target, skillInfo.Modifier);
        }
        else
        {
            throw new Exception("Error! Unknown skill type!.");
        }
        
        
    }
}