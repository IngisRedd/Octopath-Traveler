using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class HealingSkillEffect : BaseSkillEffect
{
    private double _modifier;
    private DamageType _damageType;

    public HealingSkillEffect(GameState gameState, double modifier)
        : base(gameState)
    {
        _modifier = modifier;
    }

    protected override void ApplyEffectTo(CombatUnit target)
    {
        int healingValue = CalculateHealingEffect();
        target.CurrentHP += healingValue;
        
        _gameState.CombatActionInfo.HealValues.Add(healingValue);
    }

    private int CalculateHealingEffect()
    {
        double healingValue = _gameState.CurrentUnit.ElemDef * _modifier;
        return Convert.ToInt32(healingValue);
    }
}