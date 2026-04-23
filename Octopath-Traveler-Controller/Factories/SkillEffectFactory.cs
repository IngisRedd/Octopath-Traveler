using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public static class SkillEffectFactory
{
    public static ISkillEffect Create(SkillInfo skillInfo, GameState gameState, MainConsoleView view)
    {
        if (IsItADamagingSkill(skillInfo))
        {
            return new DamageSkillEffect(gameState, view, skillInfo.Modifier, skillInfo.Type);
        }
        throw new ArgumentException($" Unknown skill name: {skillInfo.Name}!.");
    }
    private static bool IsItADamagingSkill(SkillInfo skillInfo)
        => skillInfo.Type != DamageType.None;
}