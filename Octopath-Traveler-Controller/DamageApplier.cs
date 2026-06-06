using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class DamageApplier
{
    private GameState _gameState;
    private Damage _damage;
    private SkillResultInfo _skillResultInfo;

    public DamageApplier(GameState gameState, Damage damage)
    {
        _gameState = gameState;
        _damage = damage;
    }
    
    public void Apply(CombatUnit target)
    {
        DamageTarget(target, _damage);
        RegisterDamage();
    }

    private void DamageTarget(CombatUnit target, Damage damage)
    {
        CheckForDefend(target);
        CheckForWeakness(target, damage);
        
        target.CurrentHP -= damage.Value;
        
        _skillResultInfo = new SkillResultInfo(target, ResultType.Damage, damage.Value);
    }
    
    private void CheckForDefend(CombatUnit target)
    {
        if (IsTravelerDefendingAgainstAttack(target))
        {
            _skillResultInfo.IsTargetDefending = true;
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
        if (IsBreakingPointAchieved(beast))
        {
            beast.StatusEffects[StatusType.BreakingPoint].Duration = 2;
            _gameState.CurrentTurnQueue.Remove(beast);
            _gameState.NextTurnQueue.Remove(beast);
            
            _skillResultInfo.HasEnteredBreakingPoint = true;
        }
    }
    
    private bool IsBreakingPointAchieved(Beast beast)
        => beast.CurrentShields == 0 && !beast.StatusEffects[StatusType.BreakingPoint].IsActive;

    private void RegisterDamage()
    {
        _gameState.UsedSkillResults.Add(_skillResultInfo);
    }
}
