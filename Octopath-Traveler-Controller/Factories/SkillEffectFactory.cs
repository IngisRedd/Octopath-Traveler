using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public static class SkillEffectFactory
{
    public static SkillEffectChain Create(SkillInfo skillInfo, GameState gameState, int bpToUse = 0)
    {
        if (skillInfo.Name == "Basic Attack")
        {
            List<ISkillEffect> skillEffects = new List<ISkillEffect>();
            for (int i = 0; i < (1 + bpToUse); i++)
            {
                skillEffects.Add(new DamageSkillEffect(gameState, skillInfo.Name, skillInfo.Modifier, skillInfo.Type));
            }
            return new SkillEffectChain(skillEffects);
        }
        if (skillInfo.Name == "Shooting Stars")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Name, boostedModifier, DamageType.Wind),
                new DamageSkillEffect(gameState, skillInfo.Name, boostedModifier, DamageType.Light),
                new DamageSkillEffect(gameState, skillInfo.Name, boostedModifier, DamageType.Dark)
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Heal Wounds")
        {
            decimal boostingBonus = 0.5m;
            decimal boostedModifier = skillInfo.Modifier + boostingBonus * bpToUse;

            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);

        }
        if (skillInfo.Name == "Heal More")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);

            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);

        }
        if (skillInfo.Name == "Rest")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);

        }
        if (skillInfo.Name == "First Aid")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);

        }
        if (skillInfo.Name == "Heavenly Healing")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);

        }
        if (skillInfo.Name == "Revive")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new ReviveSkillEffect(gameState, skillInfo.Name)
            };

            if (bpToUse > 0)
            {
                decimal boostedModifier = skillInfo.Modifier * bpToUse;
                skillEffectList.Add(
                    new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
                );
            }
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Vivify")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new ReviveSkillEffect(gameState, skillInfo.Name),
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Healing Touch")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new ReviveSkillEffect(gameState, skillInfo.Name),
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Revive and Rejuvenate")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new ReviveSkillEffect(gameState, skillInfo.Name),
                new HealingSkillEffect(gameState, skillInfo.Name, boostedModifier)
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Leghold Trap")
        {
            int baseEffectDuration = 2;
            int statusEffectDuration = GetBoostedDurationForStatusEffectSkill(skillInfo, bpToUse, baseEffectDuration);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new SlowDownSkillEffect(gameState, skillInfo.Name, StatusType.Slow, statusEffectDuration)
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Spearhead")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new DamageSkillEffect(gameState, skillInfo.Name, boostedModifier, skillInfo.Type),
                new GetTurnPrioritySkillEffect(gameState, skillInfo.Name)
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Last Stand")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new LastStandSkillEffect(gameState, skillInfo.Name, boostedModifier, skillInfo.Type),
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Mercy Strike")
        {
            decimal boostedModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new MercyStrikeSkillEffect(gameState, skillInfo.Name, boostedModifier, skillInfo.Type),
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (skillInfo.Name == "Vortal Claw")
        {
            List<ISkillEffect> skillEffectList = new List<ISkillEffect>
            {
                new HalveHPSkillEffect(gameState, skillInfo.Name),
            };
            return new SkillEffectChain(skillEffectList);
        }
        if (IsItADamagingSkill(skillInfo))
        {
            decimal skillModifier = skillInfo.Modifier;
            if (gameState.CurrentUnit is Traveler)
            {
                skillModifier = GetBoostedModifier((TravelerSkillInfo)skillInfo, bpToUse);
            }
            
            int numberOfHits = GetNumberOfHits(skillInfo);
            List<ISkillEffect> skillEffects = new List<ISkillEffect>();
            for (int i = 0; i < (numberOfHits); i++)
            {
                skillEffects.Add(new DamageSkillEffect(gameState, skillInfo.Name, skillModifier, skillInfo.Type));
            }
            return new SkillEffectChain(skillEffects);
        }
        throw new ArgumentException($"Unknown skill name: {skillInfo.Name}!.");
    }
    
    private static bool IsItADamagingSkill(SkillInfo skillInfo)
        => skillInfo.Type != DamageType.None;

    private static decimal GetBoostedModifier(TravelerSkillInfo skillInfo, int bpToUse)
    {
        decimal bonusPercent = SkillDescriptionParser.ParseBonusPercentage(skillInfo.Boost);
        decimal bonusRate = bonusPercent / 100m;

        return skillInfo.Modifier * (1m + (bonusRate * bpToUse));
    }

    private static int GetBoostedDurationForStatusEffectSkill(SkillInfo skillInfo, int bpToUse, int baseStatusDuration)
    {
        if (skillInfo is TravelerSkillInfo)
        {
            TravelerSkillInfo travelerSkillInfo = (TravelerSkillInfo)skillInfo;
            string afterValueMarker = " rondas";
            int boostMultiplier = SkillDescriptionParser.ParseValueBeforeMarker(travelerSkillInfo.Boost, afterValueMarker);
            return baseStatusDuration + boostMultiplier * bpToUse;
        }
        return baseStatusDuration;
    }
    
    private static int GetNumberOfHits(SkillInfo skillInfo)
    {
        if (skillInfo is BeastSkillInfo)
        {
            BeastSkillInfo beastSkillInfo = (BeastSkillInfo)skillInfo;
            return beastSkillInfo.Hits;
        }
        else
        {
            string afterValueMarker = " veces";
            int numberOfHits = SkillDescriptionParser.ParseValueBeforeMarker(skillInfo.Description, afterValueMarker);
            if (numberOfHits <= 0)
            {
                numberOfHits = 1;
            }

            return numberOfHits;
        }
    }

}