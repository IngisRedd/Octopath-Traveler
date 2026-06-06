using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class ApplyStatusEffectSkillEffect : BaseSkillEffect
{
    private StatusType _type;
    private int _duration;
    public ApplyStatusEffectSkillEffect(GameState gameState, StatusType statusType, int duration)
        : base(gameState)
    {
        _duration = duration;
        _type = statusType;
    }

    public override void ApplyTo(CombatUnit target)
    {
        target.StatusEffects[_type].Duration += _duration;
        _gameState.CurrentTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        _gameState.NextTurnQueue.ApplyPriority(target, TurnPriorityLevel.Minimun);
        
        
        SkillResultInfo resultInfo = new SkillResultInfo(target, ResultType.ApplyStatusEffect, _duration,
            statusEffectType: _type);
        _gameState.UsedSkillResults.Add(resultInfo);
    }
    

}