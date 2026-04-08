using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Skills;

public class DamagingSkill : ISkill
{
    private DamageType _type;
    private string _target;
    private double _modifier;

    public DamagingSkill(DamageType type, string target, double modifier)
    {
        _type = type;
        _target = target;
        _modifier = modifier;
    }
    
    public void Use(GameState gameState, MainConsoleView view)
    {
        Beast attackTarget = Utils.SelectTarget(gameState, view);
        int BPToUse = Utils.AskForBPToUseIfAvailable(gameState, view);

        DamageApplier damageApplier = new DamageApplier(gameState, view);
        damageApplier.UseDamagingSkill(attackTarget, _type, _modifier);
    }
}