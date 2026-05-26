using Octopath_Traveler_Model;
using Octopath_Traveler_View.ConsoleViews;
using Octopath_Traveler_View.ConsoleViews.CombatActionView;
using Octopath_Traveler_View.ResultViews;
using Octopath_Traveler;

namespace Octopath_Traveler_View;

public class CombatActionConsoleView : BaseConsoleView, ICombatActionView
{
    private readonly List<IResultView> _resultViews;
    private readonly CombatActionDataProcessor _dataProcessor;
    private readonly CombatActionHeaderView _headerView;

    public CombatActionConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _dataProcessor = new CombatActionDataProcessor(gameState);
        _headerView = new CombatActionHeaderView(view, gameState);
        
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
        _headerView.ShowHeader();

        TargetEffectsMap map = _dataProcessor.ProcessResults();
        PrintAllTargetEffects(map);

        ShowTargetsFinalHP();
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
    
    private void ShowSingleTargetEffectResult(SkillEffectResult result, int index)
    {
        foreach (IResultView resultView in _resultViews)
        {
            if (resultView.HasContent(result, index))
            {
                resultView.Render(_view, result, index);
            }
        }
    }
    
    private void ShowTargetsFinalHP()
    {
        HashSet<CombatUnit> unitsAlreadyShown = new HashSet<CombatUnit>();
        foreach (SkillEffectResult result in _gameState.AppliedSkillEffectResults)
        {
            SkillEffectResult orderedResult = _dataProcessor.GetOrderedSkillEffectResultCurrentUnitAtTheEnd(result);
            for (int i = 0; i < orderedResult.Targets.Count; i++)
            {
                ShowFinalHPIfNecessary(orderedResult, i, unitsAlreadyShown);
            }
        }
    }

    private void ShowFinalHPIfNecessary(SkillEffectResult result, int i, HashSet<CombatUnit> unitsAlreadyShown)
    {
        if (HealedOrDamagedOrResurrectedUnitHasNotBeenShown(result, i, unitsAlreadyShown))
        {
            unitsAlreadyShown.Add(result.Targets[i]);
            _view.WriteLine($"{result.Targets[i].Name} termina con HP:{result.Targets[i].CurrentHP}");
        }
    }
    
    private bool HealedOrDamagedOrResurrectedUnitHasNotBeenShown(SkillEffectResult result, int i, HashSet<CombatUnit> unitsAlreadyShown)
    {
        bool wasHealed = result.HealValues[i] != null;
        bool wasDamaged = result.Damages[i] != null;
        bool wasResurrected = result.IsTravelerResurrected[i];

        return (wasHealed || wasDamaged || wasResurrected) && !unitsAlreadyShown.Contains(result.Targets[i]);
    }
}