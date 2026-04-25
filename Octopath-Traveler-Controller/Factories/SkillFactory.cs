using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class SkillFactory
{
    public static Skill Create(SkillInfo skillInfo, GameState gameState, GameConsoleView view)
    {
        RegisterSkillUsed(skillInfo.Name, gameState);
        
        ITargetSelector selector = TargetSelectorFactory.Create(skillInfo, gameState, view);
        List<ISkillEffect> effects = SkillEffectFactory.Create(skillInfo, gameState);
        return new Skill(gameState, selector , effects);
    }

    private static void RegisterSkillUsed(string skillUsedName, GameState gameState)
    {
        gameState.SkillUsedName = skillUsedName;
    }
}