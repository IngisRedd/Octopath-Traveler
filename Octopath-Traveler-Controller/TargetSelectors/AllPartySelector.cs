using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class AllPartySelector : BaseSelector
{
    public AllPartySelector(GameState gameState)
        : base(gameState){}
    
    protected override void OnSelect()
    {
        Combatants units = GetPartyMembers();
        _gameState.CombatTargets.AddRange(units);
    }

    private Combatants GetPartyMembers()
    {
        if (_gameState.CurrentUnit is Traveler)
        {
            return _gameState.TravelerTeam;
        }
        else
        {
            return _gameState.BeastTeam;
        }
    }
}
