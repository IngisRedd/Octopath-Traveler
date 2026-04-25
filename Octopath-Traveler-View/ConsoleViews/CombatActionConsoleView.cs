using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class CombatActionConsoleView : BaseConsoleView
{
    public CombatActionConsoleView(View view, GameState gameState)
        : base(view, gameState){}

    public void ShowCombatActionResults()
    {
        ShowCombatActionType();

        SkillEffectResult orderedResult = null;
        foreach (SkillEffectResult result in _gameState.SkillEffectResults)
        {
            orderedResult = result.GetOrderedSkillEffectResultCurrentUnitAtTheEnd(_gameState.CurrentUnit);
            ShowSkillEffectResult(orderedResult);
        }

        ShowFinalHPIfNecessary(orderedResult);
    }

    private void ShowCombatActionType()
    {
        if (SkillWasUsed())
        {
            ShowSkillUsage();
        }
        else
        {
            ShowBasicAttack();
        } 
    }
    
    private bool WasUnitHealed(int? healValue)
        => healValue != null;
    
    private bool WasUnitDamaged(Damage damage)
        => damage != null;
    
    private bool SkillWasUsed()
        => _gameState.SkillUsedName != "Basic Attack";
    
    private void ShowSkillUsage()
    {
        PrintHorizontalRule();
        _view.WriteLine($"{_gameState.CurrentUnit.Name} usa {_gameState.SkillUsedName}");
    }
    
    private void ShowBasicAttack()
    {
        PrintHorizontalRule();
        _view.WriteLine($"{_gameState.CurrentUnit.Name} ataca");
    }

    public void ShowSkillEffectResult(SkillEffectResult result)
    {
        for (int i = 0; i < result.Targets.Count; i++)
        {
            if (result.IsTravelerResurrected[i])
            {
                ShowResurrection(result.Targets[i].Name);
            }
            if (WasUnitHealed(result.HealValues[i]))
            {
                ShowHealing(result.Targets[i].Name, result.HealValues[i]);
            }
            if (WasUnitDamaged(result.Damages[i]))
            {
                ShowDamageResults(result, i);
            }
        }
    }
    
    private void ShowResurrection(string targetName)
    {
        _view.WriteLine($"{targetName} revive");
    }
    
    private void ShowHealing(string targetName, int? healValue)
    {
        _view.WriteLine($"{targetName} recupera {healValue} de vida");
    }
    
    private void ShowDamageResults(SkillEffectResult result, int i)
    {
        if (result.IsTravelerDefending[i])
        {
            ShowDefense(result.Targets[i]);
        }
        if (result.Targets[i] is Beast)
        {
            ShowBeastDamageAccordingToWeakness(result, i);
        }
        else
        {
            ShowDamageReceived(result.Targets[i], result.Damages[i]);
        }

    }
    
    private void ShowDefense(CombatUnit target)
    {
        _view.WriteLine($"{target.Name} se defiende");
    }

    private void ShowBeastDamageAccordingToWeakness(SkillEffectResult result, int i)
    {
        Beast beast = (Beast)result.Targets[i];
        if (beast.IsWeakToDamageType(result.Damages[i].Type))
        {
            ShowSuperEffectiveDamageReceived(beast, result.Damages[i]);
            if (result.IsBreakingPointAchieved[i])
            {
                ShowBreakingPointAchieved(beast);
            }
        }
        else
        {
            ShowDamageReceived(result.Targets[i], result.Damages[i]);
        }
    }

    private void ShowDamageReceived(CombatUnit target, Damage damage)
    {
        if (damage.Type is DamageType.None) 
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño");
        }
        else if (damage.Type is DamageType.Phys)
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño físico");
        }
        else if (damage.Type is DamageType.Elem)
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño elemental");
        }
        else
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño de tipo {damage.Type}");
        }
    }
    
    private void ShowSuperEffectiveDamageReceived(CombatUnit attackTarget, Damage damage)
    {
        _view.WriteLine($"{attackTarget.Name} recibe {damage.Value} de daño de tipo {damage.Type} con debilidad");
    }

    private void ShowBreakingPointAchieved(Beast attackTarget)
    {
        _view.WriteLine($"{attackTarget.Name} entra en Breaking Point");
    }
    
    private void ShowFinalHPIfNecessary(SkillEffectResult result)
    {
        for (int i = 0; i < result.Targets.Count; i++)
        {
            if (WasUnitHealed(result.HealValues[i])
                || WasUnitDamaged(result.Damages[i])
                || result.IsTravelerResurrected[i])
            {
                _view.WriteLine($"{result.Targets[i].Name} termina con HP:{result.Targets[i].CurrentHP}");
            }
        }
    }
}