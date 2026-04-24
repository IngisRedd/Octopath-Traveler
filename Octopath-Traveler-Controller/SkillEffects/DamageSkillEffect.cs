using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class DamageSkillEffect : BaseSkillEffect
{
    private double _modifier;
    private DamageType _damageType;

    public DamageSkillEffect(GameState gameState, MainConsoleView view, double modifier, DamageType damageType)
        : base(gameState, view)
    {
        _modifier = modifier;
        _damageType = damageType;
    }

    protected override void ApplyEffectTo(CombatUnit target)
    {
        DamageApplier damageApplier = new DamageApplier(_gameState, _view);
        damageApplier.UseDamagingSkill(target, _damageType, _modifier);
    }
}