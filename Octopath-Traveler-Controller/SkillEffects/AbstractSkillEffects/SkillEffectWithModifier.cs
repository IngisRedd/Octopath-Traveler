using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public abstract class SkillEffectWithModifier : BaseSkillEffect
{
    protected decimal _modifier;

    public SkillEffectWithModifier(GameState gameState, string skillName, decimal modifier)
        : base(gameState, skillName)
    {
        _modifier = modifier;
    }
}