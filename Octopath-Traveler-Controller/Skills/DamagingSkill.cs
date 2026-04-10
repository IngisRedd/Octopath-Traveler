using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Skills;

public class DamagingSkill : ISkill
{
    private SkillInfo _info;

    public DamagingSkill(SkillInfo info)
    {
        _info = info;
    }
    
    public void Use(GameState gameState, MainConsoleView view)
    {
        int BPToUse = Utils.AskForBPToUseIfAvailable(gameState, view);

        List<Beast> targets = gameState.BeastTeam.AliveUnits;
        if (IsSkillSingleTarget())
        {
            targets = new List<Beast> { Utils.SelectTarget(gameState, view) };
        }
        
        view.ShowSkillUsage(_info.Name);
        DamageApplier damageApplier = new DamageApplier(gameState, view);
        damageApplier.UseDamagingSkill(targets, _info.Type, _info.Modifier);
    }
    
    private bool IsSkillSingleTarget()
        => _info.Target == SkillTarget.Single;
}