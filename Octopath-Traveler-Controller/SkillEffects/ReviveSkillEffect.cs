using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class ReviveSkillEffect : BaseSkillEffect
{
    public ReviveSkillEffect(GameState gameState)
        : base(gameState){}

    public override void ApplyTo(CombatUnit target)
    {
        if (!target.IsAlive)
        {
            target.CurrentHP = 1;
            _gameState.NextTurnQueue.Add(target);

            RegisterResurrection(target);
        }
    }
    
    private void RegisterResurrection(CombatUnit target)
    {
        SkillResultInfo resultInfo = new SkillResultInfo(target, ResultType.Revive);
        _gameState.UsedSkillResults.Add(resultInfo);
    }
}