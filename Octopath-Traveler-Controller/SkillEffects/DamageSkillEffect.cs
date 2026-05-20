using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class DamageSkillEffect : BaseSkillEffect
{
    private decimal _modifier;
    private DamageType _damageType;

    public DamageSkillEffect(GameState gameState, decimal modifier, DamageType damageType)
        : base(gameState)
    {
        _modifier = modifier;
        _damageType = damageType;
    }

    protected override void ApplyEffectTo(CombatUnit target)
    {
        DamageApplier damageApplier = new DamageApplier(_gameState);
        damageApplier.Apply(target, _damageType, _modifier);
    }
}