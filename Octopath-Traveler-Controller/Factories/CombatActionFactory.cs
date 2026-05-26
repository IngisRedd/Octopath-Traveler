using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public static class CombatActionFactory
{
    public static CombatAction Create(CombatActionType actionType, GameState gameState, IRoundView roundView, ICombatActionView combatActionView)
    {
        if (actionType == CombatActionType.Attack)
        {
            return new AttackAction(gameState, roundView, combatActionView);
        }
        if (actionType == CombatActionType.UseSkill)
        {
            return new UseSkillAction(gameState, roundView, combatActionView);
        }
        if (actionType == CombatActionType.Defend)
        {
            return new DefendAction(gameState, roundView);
        }
        if (actionType == CombatActionType.Flee)
        {
            return new FleeAction(gameState, roundView);
        }
        throw new ArgumentException($"Unknown combat action!");
    }
}