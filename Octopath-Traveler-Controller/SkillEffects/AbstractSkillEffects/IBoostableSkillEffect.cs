using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public interface IBoostableSkillEffect
{
    public void Boost(int bpToUse, string boostDescription);
}