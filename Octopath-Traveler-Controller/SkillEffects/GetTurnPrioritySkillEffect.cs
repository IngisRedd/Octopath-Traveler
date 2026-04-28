using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class GetTurnPrioritySkillEffect : BaseSkillEffect
{
    public GetTurnPrioritySkillEffect(GameState gameState)
        : base(gameState){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        _gameState.NextTurnQueue.ApplyPriority(_gameState.CurrentUnit, TurnPriorityLevel.UsedTurnPrioritySkill);
    }
}