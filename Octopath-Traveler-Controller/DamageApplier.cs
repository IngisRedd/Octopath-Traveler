using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class DamageApplier
{
    private GameState _gameState;
    private Damage _damage;

    public DamageApplier(GameState gameState, Damage damage)
    {
        _gameState = gameState;
        _damage = damage;
    }
    
    public void Apply(CombatUnit target)
    {
        DamageTarget(target, _damage);
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
        if (IsTravelerDefendingAgainstAttack(target))
        {
            Utils.SetLast(isTravelerDefending, true);
        }
        else
        {
            Utils.SetLast(isTravelerDefending, false);
        }
    }

    private bool IsTravelerDefendingAgainstAttack(CombatUnit traveler)
        => traveler.StatusEffects[StatusType.Defend].IsActive && !_damage.SkipsDefend;

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