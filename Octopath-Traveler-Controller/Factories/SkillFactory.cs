using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class SkillFactory
{
    public static Skill Create(SkillInfo skillInfo, GameState gameState, MainConsoleView view)
    {
        ITargetSelector selector = TargetSelectorFactory.Create(skillInfo, gameState, view);
        ISkillEffect effect = SkillEffectFactory.Create(skillInfo, gameState, view);
        return new Skill(selector , effect);
    }
}