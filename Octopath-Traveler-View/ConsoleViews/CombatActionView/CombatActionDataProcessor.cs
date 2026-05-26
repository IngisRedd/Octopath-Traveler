using Octopath_Traveler_Model;
using Octopath_Traveler;

namespace Octopath_Traveler_View.ConsoleViews;

public class CombatActionDataProcessor
{
    private readonly GameState _gameState;

    public CombatActionDataProcessor(GameState gameState)
    {
        _gameState = gameState;
    }

    public TargetEffectsMap ProcessResults()
    {
        TargetEffectsMap map = new TargetEffectsMap();

        foreach (SkillEffectResult result in _gameState.AppliedSkillEffectResults)
        {
            SkillEffectResult orderedResult = GetOrderedSkillEffectResultCurrentUnitAtTheEnd(result);

            for (int i = 0; i < orderedResult.Targets.Count; i++)
            {
                CombatUnit target = orderedResult.Targets[i];
                map.AddEffect(target, orderedResult, i);
            }
        }

        return map;
    }

    public SkillEffectResult GetOrderedSkillEffectResultCurrentUnitAtTheEnd(SkillEffectResult result)
    {
        SkillEffectResult newResult = result.DeepCopy();
        if (newResult.Targets.Contains(_gameState.CurrentUnit))
        {
            int index = newResult.Targets.IndexOf(_gameState.CurrentUnit);
            
            Utils.MoveItemInIndexToEnd(newResult.Targets, index);
            Utils.MoveItemInIndexToEnd(newResult.Damages, index);
            Utils.MoveItemInIndexToEnd(newResult.IsBreakingPointAchieved, index);
            Utils.MoveItemInIndexToEnd(newResult.IsTravelerDefending, index);
            Utils.MoveItemInIndexToEnd(newResult.HealValues, index);
            Utils.MoveItemInIndexToEnd(newResult.IsTravelerResurrected, index);
        }
        return newResult;   
    }
}