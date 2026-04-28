using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class SlowDownSkillEffect : BaseSkillEffect
{
    public SlowDownSkillEffect(GameState gameState)
        : base(gameState){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        int slowDuration = 2;
        target.StatusEffects[StatusType.Slow].Duration += slowDuration;
        _gameState.CurrentTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        _gameState.NextTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        
        List<int?> slowedTurns = _gameState.LastSkillEffectResult.TurnsSlowedTarget;
        Utils.SetLast(slowedTurns, slowDuration);
    }
}