using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Skills;

internal class SingleTargetDamagingBeastSkill : ISkill
{
    private BeastSkillInfo _info;
    private Stat _targetStat;
    private SelectionType _selectionType;
    
    public SingleTargetDamagingBeastSkill(BeastSkillInfo info, Stat targetStat, SelectionType selectionType)
    {
        _info = info;
        _targetStat = targetStat;
        _selectionType = selectionType;
    }
    
    public void Use(GameState gameState, MainConsoleView view)
    {
        List<Traveler> possibleTargets = gameState.TravelerTeam.AliveUnits;
        Traveler target = BeastTargetSelector.SelectTarget(possibleTargets, _targetStat, _selectionType);
        view.ShowSkillUsage(_info.Name);
        DamageApplier damageApplier = new DamageApplier(gameState, view);
        damageApplier.UseDamagingSkill(new []{ target }, _info.Type, _info.Modifier);
    }
}