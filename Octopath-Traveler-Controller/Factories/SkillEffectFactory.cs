using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public static class SkillEffectFactory
{
    public static List<ISkillEffect> Create(SkillInfo skillInfo, GameState gameState)
    {
        if (IsItADamagingSkill(skillInfo))
        {
            return new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Modifier, skillInfo.Type)
            };
        }
        if (skillInfo.Name == "Heal Wounds")
        {
            return new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Modifier)
            };
        }
        if (skillInfo.Name == "Heal More")
        {
            return new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Modifier)
            };
        }
        if (skillInfo.Name == "Rest")
        {
            return new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Modifier)
            };
        }
        if (skillInfo.Name == "First Aid")
        {
            return new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Modifier)
            };
        }
        if (skillInfo.Name == "Heavenly Healing")
        {
            return new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Modifier)
            };
        }
        if (skillInfo.Name == "Revive")
        {
            return new List<ISkillEffect>
            {
                new ReviveSkillEffect(gameState)
            };
        }
        throw new ArgumentException($" Unknown skill name: {skillInfo.Name}!.");
    }
    private static bool IsItADamagingSkill(SkillInfo skillInfo)
        => skillInfo.Type != DamageType.None;
}