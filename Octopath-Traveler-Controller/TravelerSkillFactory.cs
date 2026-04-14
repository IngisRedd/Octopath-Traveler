using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class TravelerSkillFactory : SkillFactory
{
    public override ISkill Create(SkillInfo skillInfo)
    {
        TravelerSkillInfo travelerSkillInfo = (TravelerSkillInfo)skillInfo;
        if (IsItADamagingSkill(travelerSkillInfo))
        {
            return new DamagingSkill(travelerSkillInfo);
        }
        else
        {
            throw new Exception("Error! Unknown skill type!.");
        }
    }
}