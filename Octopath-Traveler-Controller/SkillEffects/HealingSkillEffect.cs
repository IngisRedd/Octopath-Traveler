using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class HealingSkillEffect : SkillEffectWithModifier
{
    public HealingSkillEffect(GameState gameState, decimal modifier)
        : base(gameState, modifier){}

    public override void ApplyTo(CombatUnit target)
    {
        int healValue = CalculateHealingEffect();
        target.CurrentHP += healValue;
        
        RegisterHealing(_gameState, target, healValue);
    }

    private int CalculateHealingEffect()
    {
        decimal healingValue = _gameState.CurrentUnit.ElemDef * _modifier;
        return (int)Math.Floor(healingValue);
    }
    
    public static void RegisterHealing(GameState gameState, CombatUnit target,  int healValue)
    {
        SkillResultInfo resultInfo = new SkillResultInfo(target, ResultType.Heal, healValue);
        gameState.UsedSkillResults.Add(resultInfo);
    }
}