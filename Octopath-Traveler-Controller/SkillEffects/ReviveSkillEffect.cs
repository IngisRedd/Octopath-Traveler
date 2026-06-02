using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class ReviveSkillEffect : BaseSkillEffect
{
    public ReviveSkillEffect(GameState gameState, string skillName)
        : base(gameState, skillName){}

    protected override void ApplyEffectTo(CombatUnit target)
    {
        if (!target.IsAlive)
        {
            target.CurrentHP = 1;
            _gameState.NextTurnQueue.Add(target);

            RegisterResurrection();
        }
    }
    
    private void RegisterResurrection()
    {
        List<bool> isTravelerResurrected = _gameState.LastSkillEffectResult.IsTravelerResurrected;
        Utils.SetLast(isTravelerResurrected, true);
    }
}