using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class ReviveSkillEffect : BaseSkillEffect
{
    public ReviveSkillEffect(GameState gameState)
        : base(gameState){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        target.CurrentHP = 1;
        _gameState.NextTurnQueue.Add(target);
        _gameState.CombatActionInfo.AreThereResurrections = true;
    }
    
}