using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class DamageApplier
{
    private GameState _gameState;
    private MainConsoleView _view;
    private DamageActionResultInfo _resultInfo = new DamageActionResultInfo();


    public DamageApplier(GameState gameState, MainConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }
    
    public void MakeBasicAttack(CombatUnit target, DamageType weapon)
    {
        double basicAttackModifier = 1.3;
        DamageCalculator damageCalculator =
            new DamageCalculator(basicAttackModifier, _gameState.CurrentUnit, target, weapon);
        Damage damage = damageCalculator.Calculate();
        DamageTarget(target, damage);
        _resultInfo.Targets = new() { target };
        _view.ShowDamageResults(_resultInfo);
    }

    
    public void UseDamagingSkill(IEnumerable<CombatUnit> targets, DamageType type, double modifier)
    {
        foreach (CombatUnit target in targets)
        {
            DamageCalculator damageCalculator =
                new DamageCalculator(modifier, _gameState.CurrentUnit, target, type);
            Damage damage = damageCalculator.Calculate();
            DamageTarget(target, damage);
        }
        
        _resultInfo.Targets = targets.ToList();
        _view.ShowDamageResults(_resultInfo);
    }
    
    private void DamageTarget(CombatUnit target, Damage damage)
    {
        if (target is Beast)
        {
            Beast beast = (Beast)target;
            if (beast.IsWeakToDamageType(damage.Type))
            {
                ApplySupperEffectiveDamage(beast, damage);
                return;
            }
        }
        target.CurrentHP -= damage.Value;
        _resultInfo.Damages.Add(damage);
        _resultInfo.IsBreakingPointAchieved.Add(false);
    }

    private void ApplySupperEffectiveDamage(Beast beast, Damage damage)
    {
        double weaknessModifier = 1.5;
        Damage newDamage = DamageCalculator.ApplyModifier(damage, weaknessModifier);
        _resultInfo.Damages.Add(newDamage);
        beast.CurrentHP -= newDamage.Value;
        beast.CurrentShields -= 1;
        if (IsBreakingPointAchieved(beast))
        {
            _resultInfo.IsBreakingPointAchieved.Add(true);
            beast.StatusEffects[StatusType.BreakingPoint].Duration = 1;
        }
        else
        {
            _resultInfo.IsBreakingPointAchieved.Add(false);
        }
    }

    private bool IsBreakingPointAchieved(Beast beast)
        => beast.CurrentShields == 0;
}