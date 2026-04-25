using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class DamageApplier
{
    private GameState _gameState;

    public DamageApplier(GameState gameState)
    {
        _gameState = gameState;
    }
    
    public void Apply(CombatUnit target, DamageType type, double modifier)
    {
        DamageCalculator damageCalculator =
            new DamageCalculator(modifier, _gameState.CurrentUnit, target, type);
        Damage damage = damageCalculator.Calculate();
        DamageTarget(target, damage);
    }
    
    private void DamageTarget(CombatUnit target, Damage damage)
    {
        CheckForDefend(target);
        CheckForWeakness(target, damage);
        
        target.CurrentHP -= damage.Value;
        List<Damage> damages = _gameState.LastSkillEffectResult.Damages;
        Utils.SetLast(damages, damage);
    }
    
    private void CheckForDefend(CombatUnit target)
    {
        List<bool> isTravelerDefending = _gameState.LastSkillEffectResult.IsTravelerDefending;
        if (target.StatusEffects[StatusType.Defend].IsActive)
        {
            Utils.SetLast(isTravelerDefending, true);
        }
        else
        {
            Utils.SetLast(isTravelerDefending, false);
        }
    }

    private void CheckForWeakness(CombatUnit target, Damage damage)
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
        List<bool> isBreakingPointAchieved = _gameState.LastSkillEffectResult.IsBreakingPointAchieved;
        if (IsBreakingPointAchieved(beast))
        {
            beast.StatusEffects[StatusType.BreakingPoint].Duration = 2;
            _gameState.CurrentTurnQueue.Remove(beast);
            _gameState.NextTurnQueue.Remove(beast);
            
            Utils.SetLast(isBreakingPointAchieved, true);
        }
        else
        {
            Utils.SetLast(isBreakingPointAchieved, false);
        }
    }
    
    private bool IsBreakingPointAchieved(Beast beast)
        => beast.CurrentShields == 0 && !beast.StatusEffects[StatusType.BreakingPoint].IsActive;
}