using Octopath_Traveler_Model;
using Octopath_Traveler_View.ConsoleViews;
using Octopath_Traveler_View.ConsoleViews.CombatActionView;
using Octopath_Traveler_View.ResultViews;
using Octopath_Traveler;

namespace Octopath_Traveler_View;

public class SkillResultsConsoleView : BaseConsoleView, ICombatActionView
{
    private readonly CombatActionHeaderView _headerView;

    public SkillResultsConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _headerView = new CombatActionHeaderView(view, gameState);
    }

    public void ShowCombatActionResults()
    {
        _headerView.ShowHeader();
        RenderUsedSkillResults();
        ShowTargetsFinalHP();
    }

    public void RenderUsedSkillResults()
    {
        foreach (SkillResultInfo skillResult in _gameState.UsedSkillResults)
        {
              ISkillResultView resultView = SkillResultViewFactory.Create(_view, skillResult);
              resultView.Render();
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