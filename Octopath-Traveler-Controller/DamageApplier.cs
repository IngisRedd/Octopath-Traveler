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
        CheckForAndApplyWeakness(target, damage);
        
        target.CurrentHP -= damage.Value;
        _gameState.CombatActionInfo.Damages.Add(damage);        
    }

    private void CheckForAndApplyWeakness(CombatUnit target, Damage damage)
    {
        if (target is Beast)
        {
            Beast beast = (Beast)target;
            if (beast.IsWeakToDamageType(damage.Type))
            {
                if (damage.Value > 0)
                {
                    beast.CurrentShields -= 1;
                }
                CheckForAndApplyBreakingPoint(beast);
            }
        }
    }
    
    private void CheckForAndApplyBreakingPoint(Beast beast)
    {
        if (IsBreakingPointAchieved(beast))
        {
            beast.StatusEffects[StatusType.BreakingPoint].Duration = 2;
            _gameState.CurrentTurnQueue.Remove(beast);
            _gameState.NextTurnQueue.Remove(beast);
            
            _gameState.CombatActionInfo.IsBreakingPointAchieved.Add(true);
        }
        else
        {
            _gameState.CombatActionInfo.IsBreakingPointAchieved.Add(false);
        }
    }

    private void CheckForDefend(CombatUnit target)
    {
        if (target.StatusEffects[StatusType.Defend].IsActive)
        {
            _gameState.CombatActionInfo.IsTravelerDefending.Add(true);
        }
        else
        {
            _gameState.CombatActionInfo.IsTravelerDefending.Add(false);
        }
    }
    
    private bool IsBreakingPointAchieved(Beast beast)
        => beast.CurrentShields == 0 && !beast.StatusEffects[StatusType.BreakingPoint].IsActive;
}