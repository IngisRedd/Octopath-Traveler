using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Skills;

public class DamagingSkill : ISkill
{
    private TravelerSkillInfo _info;

    public DamagingSkill(TravelerSkillInfo info)
    {
        _info = info;
    }
    
    public void Use(GameState gameState, MainConsoleView view)
    {
        List<Beast> targets = gameState.BeastTeam.AliveUnits;
        if (IsSkillSingleTarget())
        {
            targets = new List<Beast> { Utils.SelectTarget(gameState, view) };
        }
        int BPToUse = Utils.AskForBPToUseIfAvailable(gameState, view);
        
        view.ShowSkillUsage(_info.Name);
        gameState.CurrentTraveler.CurrentSP -= _info.SP;
        DamageApplier damageApplier = new DamageApplier(gameState, view);
        damageApplier.UseDamagingSkill(targets, _info.Type, _info.Modifier);
    }
    
    private bool IsSkillSingleTarget()  // Repeated code
        => _info.Target == SkillTarget.Single;
}