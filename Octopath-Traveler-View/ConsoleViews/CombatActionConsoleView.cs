using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class CombatActionConsoleView : BaseConsoleView
{
    public CombatActionConsoleView(View view, GameState gameState)
        : base(view, gameState){}

    public void ShowCombatActionResults()
    {
        if (SkillWasUsed())
        {
            ShowSkillUsage(_gameState.CombatActionInfo.SkillName);
        }
        else
        {
            ShowBasicAttack();
        }
        
        if (_gameState.CombatActionInfo.HealValues.Count > 0)
        {
            ShowHealResults();
        }
        else if (_gameState.CombatActionInfo.AreThereResurrections)
        {
            ShowResurrectionResults();
        }
        else
        {
            ShowDamageResults();
        }

        foreach (CombatUnit target in GetOrderedTargetListCurrentUnitAtTheEnd())
        {
            ShowFinalHP(target);
        }
    }

    private bool SkillWasUsed()
        => _gameState.CombatActionInfo.SkillName != null;
    
    private void ShowSkillUsage(string skillName)
    {
        PrintHorizontalRule();
        _view.WriteLine($"{_gameState.CurrentUnit.Name} usa {skillName}");
    }
    
    private void ShowBasicAttack()
    {
        PrintHorizontalRule();
        _view.WriteLine($"{_gameState.CurrentUnit.Name} ataca");
    }

    private void ShowHealResults()
    {
        List<CombatUnit> targets = GetOrderedTargetListCurrentUnitAtTheEnd();
        for (int i = 0; i < targets.Count; i++)
        {
            string targetName = targets[i].Name;
            int healValue = _gameState.CombatActionInfo.HealValues[i];
            _view.WriteLine($"{targetName} recupera {healValue} de vida");
        }
    }
    
    private void ShowResurrectionResults()
    {
        foreach (CombatUnit target in GetOrderedTargetListCurrentUnitAtTheEnd())
        {
            _view.WriteLine($"{target.Name} revive");
        }
    }

    private List<CombatUnit> GetOrderedTargetListCurrentUnitAtTheEnd()
    {
        List<CombatUnit> units = _gameState.CombatTargets.ToList();
        if (units.Contains(_gameState.CurrentUnit))
        {
            units.Remove(_gameState.CurrentUnit);
            units.Add(_gameState.CurrentUnit);
        }
        return units;
    }
    
    private void ShowDamageResults()
    {
        List<CombatUnit> targets = GetOrderedTargetListCurrentUnitAtTheEnd();
        CombatActionInfo combatInfo = _gameState.CombatActionInfo;
        for (int i = 0; i < targets.Count; i++)
        {
            if (combatInfo.IsTravelerDefending[i])
            {
                ShowDefense(targets[i]);
            }
            if (targets[i] is Beast)
            {
                Beast beast = (Beast)targets[i];
                if (beast.IsWeakToDamageType(combatInfo.Damages[i].Type))
                {
                    ShowSuperEffectiveDamageReceived(beast, combatInfo.Damages[i]);
                    if (combatInfo.IsBreakingPointAchieved[i])
                    {
                        ShowBreakingPointAchieved(beast);
                    }
                    continue;
                }
            }
            ShowDamageReceived(targets[i], combatInfo.Damages[i]);
        } 
    }

    private void ShowDefense(CombatUnit target)
    {
        _view.WriteLine($"{target.Name} se defiende");
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
    
    private void ShowFinalHP(CombatUnit attackTarget)
    {
        _view.WriteLine($"{attackTarget.Name} termina con HP:{attackTarget.CurrentHP}");
    }
}