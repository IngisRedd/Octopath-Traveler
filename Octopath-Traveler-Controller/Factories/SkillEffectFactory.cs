using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public static class SkillEffectFactory
{
    public static SkillEffectsChain Create(SkillInfo skillInfo, GameState gameState, IRoundView view)
    {
        if (skillInfo.Name == "Shooting Stars")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, DamageType.Wind),
                new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, DamageType.Light),
                new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, DamageType.Dark)
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Heal Wounds")
        {
            return CreateHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Heal More")
        {
            return CreateHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Rest")
        {
            return CreateHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "First Aid")
        {
            return CreateHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Heavenly Healing")
        {
            return CreateHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Revive")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new ReviveSkillEffect(gameState, skillInfo.Name)
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Vivify")
        {
            return CreateReviveAndHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Healing Touch")
        {
            return CreateReviveAndHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Revive and Rejuvenate")
        {
            return CreateReviveAndHealingEffect(skillInfo, gameState);
        }
        if (skillInfo.Name == "Leghold Trap")
        {
            int slowedTurns = 2;
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new SlowDownSkillEffect(gameState, skillInfo.Name, slowedTurns)
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Spearhead")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, skillInfo.Type),
                new GetTurnPrioritySkillEffect(gameState, skillInfo.Name)
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Last Stand")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new LastStandSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, skillInfo.Type),
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Mercy Strike")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new MercyStrikeSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, skillInfo.Type),
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Vortal Claw")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HalveHPSkillEffect(gameState, skillInfo.Name),
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (skillInfo.Name == "Nightmare Chimera")
        {
            List<DamageType> weapons = new List<DamageType>
            {
                DamageType.Sword, DamageType.Spear, DamageType.Dagger, DamageType.Axe, DamageType.Bow, DamageType.Stave
            };
            DamageType weaponType = view.SelectWeapon(weapons);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, weaponType)
            };
            return new SkillEffectsChain(skillEffectList);
        }
        if (IsItADamagingSkill(skillInfo))
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, skillInfo.Type)
            };
            return new SkillEffectsChain(skillEffectList);
        }
        throw new ArgumentException($"Unknown skill name: {skillInfo.Name}!.");
    }

    private static SkillEffectsChain CreateHealingEffect(SkillInfo skillInfo, GameState gameState)
    {
        List<ISkillEffect> skillEffectList = new List<ISkillEffect>
        {
            new HealingSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier)
        };
        return new SkillEffectsChain(skillEffectList);
    }

    private static SkillEffectsChain CreateReviveAndHealingEffect(SkillInfo skillInfo, GameState gameState)
    {
        List<ISkillEffect> skillEffectList = new List<ISkillEffect>
        {
            new ReviveSkillEffect(gameState, skillInfo.Name),
            new HealingSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier)
        };
        return new SkillEffectsChain(skillEffectList);
    }

    private static bool IsItADamagingSkill(SkillInfo skillInfo)
        => skillInfo.Type != DamageType.None;

}