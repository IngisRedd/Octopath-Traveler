using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public abstract class ConditionApplierSkillEffect : BaseSkillEffect, IBoostableSkillEffect
{
    protected int _duration;

    public ConditionApplierSkillEffect(GameState gameState, string skillName, int duration)
        : base(gameState, skillName)
    {
        _duration = duration;
    }

    
    public void Boost(int bpToUse, string boostDescription)
    {
        int turnBonus = BoostDescriptionParser.ParseConditionDurationBonus(boostDescription);
        _duration += bpToUse * turnBonus;
    }

}