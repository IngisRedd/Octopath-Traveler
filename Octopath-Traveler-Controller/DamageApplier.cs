using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class DamageApplier
{
    private GameState _gameState;
    private MainConsoleView _view;


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
    }

    
    public void UseDamagingSkill(CombatUnit target, DamageType type, double modifier)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(modifier, _gameState.CurrentUnit, target, type);
        Damage damage = damageCalculator.Calculate();
        DamageTarget(target, damage);
    }
    
    private void DamageTarget(CombatUnit target, Damage damage)
    {
        CheckForDefend(target);
        if (target is Beast)
        {
            Beast beast = (Beast)target;
            if (beast.IsWeakToDamageType(damage.Type))
            {
                _view.ShowSuperEffectiveDamageReceived(target, damage);
                if (damage.Value > 0)
                {
                    beast.CurrentShields -= 1;
                }
                CheckForAndApplyBreakingPoint(beast);
            }
            else
            {
                _view.ShowDamageReceived(target, damage);
            }
        }
        else
        {
            _view.ShowDamageReceived(target, damage);
        }
        
        target.CurrentHP -= damage.Value;
    }


    
    private void CheckForAndApplyBreakingPoint(Beast beast)
    {
        if (IsBreakingPointAchieved(beast))
        {
            _view.ShowBreakingPointAchieved(beast);
            beast.StatusEffects[StatusType.BreakingPoint].Duration = 2;
            _gameState.CurrentTurnQueue.Remove(beast);
            _gameState.NextTurnQueue.Remove(beast);
        }
    }

    private void CheckForDefend(CombatUnit target)
    {
        if (target.StatusEffects[StatusType.Defend].IsActive)
        {
            _view.ShowDefense(target);
        }
    }
    
    private bool IsBreakingPointAchieved(Beast beast)
        => beast.CurrentShields == 0 && !beast.StatusEffects[StatusType.BreakingPoint].IsActive;
}