using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Skills;

public class MultiTargetDamagingBeastSkill : ISkill
{
    private BeastSkillInfo _info;

    public MultiTargetDamagingBeastSkill(BeastSkillInfo info)
    {
        _info = info;
    }
    
    public void Use(GameState gameState, MainConsoleView view)
    {
        List<Traveler> targets = gameState.TravelerTeam.AliveUnits;
        view.ShowSkillUsage(_info.Name);
        DamageApplier damageApplier = new DamageApplier(gameState, view);
        damageApplier.UseDamagingSkill(targets, _info.Type, _info.Modifier);
    }
    
}