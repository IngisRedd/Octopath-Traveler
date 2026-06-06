using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class GetTurnPrioritySkillEffect : BaseSkillEffect
{
    public GetTurnPrioritySkillEffect(GameState gameState)
        : base(gameState){}

    public override void ApplyTo(CombatUnit target)
    {
        _gameState.NextTurnQueue.ApplyPriority(_gameState.CurrentUnit, TurnPriorityLevel.UsedTurnPrioritySkill);
    }
}