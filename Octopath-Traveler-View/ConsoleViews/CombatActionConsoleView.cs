using Octopath_Traveler_Model;
using Octopath_Traveler_View.ResultViews;
using Octopath_Traveler;

namespace Octopath_Traveler_View;

public class CombatActionConsoleView : BaseConsoleView
{
    private List<IResultView> _resultViews;
    public CombatActionConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _resultViews = new List<IResultView>
        {
            new ResurrectionResultView(),
            new HealingResultView(),
            new SlowedResultView(),
            new DamageResultView()
        };
    }

    public void ShowCombatActionResults()
    {
        ShowCombatActionType();

        TargetEffectsMap map = BuildTargetEffectsMap();
        PrintAllTargetEffects(map);

        ShowTargetsFinalHP();
    }
    
    private void ShowCombatActionType()
    {
        if (WasSkillUsed())
        {
            ShowSkillUsage();
        }
        else
        {
            ShowBasicAttack();
        } 
    }

    private bool WasSkillUsed()
        => _gameState.SkillUsedName != "Basic Attack";
    
    private void ShowSkillUsage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"{_gameState.CurrentUnit.Name} usa {_gameState.SkillUsedName}");
    }
    
    private void ShowBasicAttack()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"{_gameState.CurrentUnit.Name} ataca");
    }
    
    private TargetEffectsMap BuildTargetEffectsMap()
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
    
    private SkillEffectResult GetOrderedSkillEffectResultCurrentUnitAtTheEnd(SkillEffectResult result)
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

    private void PrintAllTargetEffects(TargetEffectsMap map)
    {
        foreach (CombatUnit target in map.DistinctTargetsInOrder)
        {
            List<TargetedEffect> targetEffects = map.EffectsByTarget[target];
            PrintSingleTargetEffects(targetEffects);
        }
    }

    private void PrintSingleTargetEffects(List<TargetedEffect> targetEffects)
    {
        foreach (TargetedEffect effect in targetEffects)
        {
            ShowSingleTargetEffectResult(effect.Result, effect.Index);
        }
    }
    
    public void ShowSingleTargetEffectResult(SkillEffectResult result, int index)
    {
        foreach (IResultView resultView in _resultViews)
        {
            if (resultView.HasContent(result, index))
            {
                resultView.Render(_view, result, index);
            }
        }
    }
    
    private bool WasUnitHealed(int? healValue)
        => healValue != null;
    
    private bool WasUnitDamaged(Damage damage)
        => damage != null;
    
    private void ShowTargetsFinalHP()
    {
        HashSet<CombatUnit> unitsAlreadyShown = new HashSet<CombatUnit>();
        foreach (SkillEffectResult result in _gameState.AppliedSkillEffectResults)
        {
            SkillEffectResult orderedResult = GetOrderedSkillEffectResultCurrentUnitAtTheEnd(result);
            for (int i = 0; i < orderedResult.Targets.Count; i++)
            {
                ShowFinalHPIfNecessary(orderedResult, i, unitsAlreadyShown);
            }
        }
    }

    private void ShowFinalHPIfNecessary(SkillEffectResult result, int i,
        HashSet<CombatUnit> unitsAlreadyShown)
    {
        if (HealedOrDamagedOrResurrectedUnitHasentBeenShown(result, i, unitsAlreadyShown))
        {
            unitsAlreadyShown.Add(result.Targets[i]);
            _view.WriteLine($"{result.Targets[i].Name} termina con HP:{result.Targets[i].CurrentHP}");
        }

    }
    
    private bool HealedOrDamagedOrResurrectedUnitHasentBeenShown(SkillEffectResult result, int i,
        HashSet<CombatUnit> unitsAlreadyShown)
        => (WasUnitHealed(result.HealValues[i])
            || WasUnitDamaged(result.Damages[i])
            || result.IsTravelerResurrected[i])
           && !unitsAlreadyShown.Contains(result.Targets[i]);
}