using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public abstract class StatusEffectApplierSkillEffect : BaseSkillEffect
{
    protected StatusType _type;
    protected int _duration;

    public StatusEffectApplierSkillEffect(GameState gameState, string skillName, StatusType statusType, int duration)
        : base(gameState, skillName)
    {
        _duration = duration;
        _type = statusType;
    }
}