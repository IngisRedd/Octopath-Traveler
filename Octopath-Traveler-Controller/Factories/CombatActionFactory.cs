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
            TravelerSkillInfo basicAttackSkillInfo = CreateBasicAttackSkillInfo(gameState, roundView);
            return new UseSkillAction(gameState, roundView, combatActionView, basicAttackSkillInfo);
        }
        if (actionType == CombatActionType.UseSkill)
        {
            TravelerSkillInfo selectedSkillInfo = roundView.SelectFromAvailableSkills();
            TravelerSkillInfoConfigurator.Configure(selectedSkillInfo, roundView);

            return new UseSkillAction(gameState, roundView, combatActionView, selectedSkillInfo);
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
    
    private static TravelerSkillInfo CreateBasicAttackSkillInfo(GameState gameState, IRoundView roundView)
    {
        decimal basicAttackModifier = 1.3m;
        DamageType selectedWeapon = roundView.SelectWeapon(gameState.CurrentTraveler.Weapons);

        return new TravelerSkillInfo
        {
            Name = "Basic Attack",
            Type = selectedWeapon,
            Description = "",
            Modifier = basicAttackModifier,
            Target = SkillTarget.Single,
            SP = 0,
            Boost = ""
        };
    }

}