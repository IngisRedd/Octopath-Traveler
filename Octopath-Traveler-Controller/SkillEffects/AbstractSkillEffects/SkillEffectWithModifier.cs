using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public abstract class SkillEffectWithModifier : BaseSkillEffect, IBoostableSkillEffect
{
    protected decimal _modifier;

    public SkillEffectWithModifier(GameState gameState, string skillName, decimal modifier)
        : base(gameState, skillName)
    {
        _modifier = modifier;
    }
    
    public void Boost(int bpToUse, string boostDescription)
    {
            decimal bonusPercent = BoostDescriptionParser.ParseBonusPercentage(boostDescription);
            decimal bonusRate = bonusPercent / 100m;

            _modifier = _modifier * (1m + (bonusRate * bpToUse));
    }

}