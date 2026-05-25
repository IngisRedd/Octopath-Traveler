using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public static class CombatActionFactory
{
    public static CombatAction Create(CombatActionType actionType, GameState gameState, RoundConsoleView roundConsoleView, CombatActionConsoleView combatActionConsoleView)
    {
        if (actionType == CombatActionType.Attack)
        {
            return new AttackAction(gameState, roundConsoleView, combatActionConsoleView);
        }
        if (actionType == CombatActionType.UseSkill)
        {
            return new UseSkillAction(gameState, roundConsoleView, combatActionConsoleView);
        }
        if (actionType == CombatActionType.Defend)
        {
            return new DefendAction(gameState, roundConsoleView);
        }
        if (actionType == CombatActionType.Flee)
        {
            return new FleeAction(gameState, roundConsoleView);
        }
        throw new ArgumentException($"Unknown combat action!");
    }
}