using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;
using Octopath_Traveler.TargetSelectors;

namespace Octopath_Traveler;

public static class TargetSelectorFactory
{
    public static ITargetSelector Create(SkillInfo skillInfo, GameState gameState, GameConsoleView view)
    {
        if (skillInfo.Name == "Attack")
        {
            return new BeastSingleEnemySelector(gameState, Stat.HP, SelectionType.Highest);
        }
        if (skillInfo.Name == "Befuddling claw")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemAtk, SelectionType.Highest);
        }
        if (skillInfo.Name == "Stab")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Meteor Storm")
        {
            return new BeastSingleEnemySelector(gameState, Stat.Speed, SelectionType.Highest);
        }
        if (skillInfo.Name == "Freeze")
        {
            return new BeastSingleEnemySelector(gameState, Stat.Speed, SelectionType.Highest);
        }
        if (skillInfo.Name == "Luminescence")
        {
            return new BeastSingleEnemySelector(gameState, Stat.Speed, SelectionType.Highest);
        }
        if (skillInfo.Name == "Enshadow")
        {
            return new BeastSingleEnemySelector(gameState, Stat.Speed, SelectionType.Highest);
        }
        if (skillInfo.Name == "Wind slash")
        {
            return new BeastSingleEnemySelector(gameState, Stat.Speed, SelectionType.Highest);
        }
        if (skillInfo.Name == "Boar Rush")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Windshot")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Firesand")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Thundershot")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Lightshot")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Iceshot")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Shadowshot")
        {
            return new BeastSingleEnemySelector(gameState, Stat.ElemDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Vorpal Fang")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Double Bite")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Triple Slash")
        {
            return new BeastSingleEnemySelector(gameState, Stat.HP, SelectionType.Highest);
        }
        if (skillInfo.Name == "Consume Armor")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysDef, SelectionType.Highest);
        }
        if (skillInfo.Name == "Flap")
        {
            return new BeastSingleEnemySelector(gameState, Stat.HP, SelectionType.Highest);
        }
        if (skillInfo.Name == "Acid Spray")
        {
            return new BeastSingleEnemySelector(gameState, Stat.HP, SelectionType.Highest);
        }
        if (skillInfo.Name == "Gather Strength")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysDef, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Soporific Sting")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysAtk, SelectionType.Highest);
        }
        if (skillInfo.Name == "Soporific Strike")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysAtk, SelectionType.Highest);
        }
        if (skillInfo.Name == "Constrictor")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysAtk, SelectionType.Highest);
        }
        if (skillInfo.Name == "Savage Fang")
        {
            return new BeastSingleEnemySelector(gameState, Stat.PhysAtk, SelectionType.Highest);
        }
        if (skillInfo.Name == "Poison Strike")
        {
            return new BeastSingleEnemySelector(gameState, Stat.HP, SelectionType.Highest);
        }
        if (skillInfo.Name == "Soporific Fang")
        {
            return new BeastSingleEnemySelector(gameState, Stat.Speed, SelectionType.Lowest);
        }
        if (skillInfo.Name == "Web Storm")
        {
            return new BeastSingleEnemySelector(gameState, Stat.HP, SelectionType.Highest);
        }
        if (skillInfo.Name == "Revive")
        {
            return new DeadPartySelector(gameState);
        }
        if (skillInfo.Name == "Vivify")
        {
            return new SingleDeadAllySelector(gameState, view);
        }
        if (skillInfo.Name == "Healing Touch")
        {
            return new AllPartySelector(gameState);
        }
        if (skillInfo.Name == "Revive and Rejuvenate")
        {
            return new AllPartySelector(gameState);
        }
        if (IsSkillTargetEnemies(skillInfo))
        {
            return new AllEnemiesSelector(gameState);
        }
        if (IsSkillTargetParty(skillInfo))
        {
            return new AlivePartySelector(gameState);
        }
        if (gameState.CurrentUnit is Traveler && IsSkillTargetSingle(skillInfo))
        {
            return new TravelerSingleEnemySelector(gameState, view);
        }
        if (gameState.CurrentUnit is Traveler && IsSkillTargetAlly(skillInfo))
        {
            return new SingleAllySelector(gameState, view);
        }
        throw new ArgumentException($" Unknown skill name: {skillInfo.Name}!.");
    }
    
    private static bool IsSkillTargetEnemies(SkillInfo skillInfo)
        => skillInfo.Target == SkillTarget.Enemies;
    private static bool IsSkillTargetParty(SkillInfo skillInfo)
        => skillInfo.Target == SkillTarget.Party;
    private static bool IsSkillTargetSingle(SkillInfo skillInfo)
        => skillInfo.Target == SkillTarget.Single;
    private static bool IsSkillTargetAlly(SkillInfo skillInfo)
        => skillInfo.Target == SkillTarget.Ally;
}