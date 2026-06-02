using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class SlowDownSkillEffect : StatusEffectApplierSkillEffect
{
    public SlowDownSkillEffect(GameState gameState, string skillName, StatusType statusType, int duration)
        : base(gameState, skillName, statusType, duration){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        target.StatusEffects[_type].Duration += _duration;
        _gameState.CurrentTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        _gameState.NextTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        
        List<int?> slowedTurns = _gameState.LastSkillEffectResult.TurnsSlowedTarget;
        Utils.SetLast(slowedTurns, _duration);
    }
}