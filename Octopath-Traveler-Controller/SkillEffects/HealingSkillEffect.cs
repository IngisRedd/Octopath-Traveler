using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class HealingSkillEffect : BaseSkillEffect
{
    private decimal _modifier;
    private DamageType _damageType;

    public HealingSkillEffect(GameState gameState, decimal modifier)
        : base(gameState)
    {
        _modifier = modifier;
    }

    protected override void ApplyEffectTo(CombatUnit target)
    {
        int healValue = CalculateHealingEffect();
        target.CurrentHP += healValue;
        
        RegisterHealing(healValue);
    }

    private int CalculateHealingEffect()
    {
        decimal healingValue = _gameState.CurrentUnit.ElemDef * _modifier;
        return (int)Math.Floor(healingValue);
    }
    
    private void RegisterHealing(int healValue)
    {
        List<int?> healValues = _gameState.LastSkillEffectResult.HealValues;
        Utils.SetLast(healValues, healValue);
    }
}