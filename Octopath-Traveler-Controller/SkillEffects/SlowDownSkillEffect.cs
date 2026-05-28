using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class SlowDownSkillEffect : ConditionApplierSkillEffect
{
    public SlowDownSkillEffect(GameState gameState, string skillName, int duration)
        : base(gameState, skillName, duration){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        target.StatusEffects[StatusType.Slow].Duration += _duration;
        _gameState.CurrentTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        _gameState.NextTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        
        List<int?> slowedTurns = _gameState.LastSkillEffectResult.TurnsSlowedTarget;
        Utils.SetLast(slowedTurns, _duration);
    }
}